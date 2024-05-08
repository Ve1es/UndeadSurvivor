using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegularZombie : Enemy
{
    private const float Angle0 = 0;
    private const float Angle180 = 180;
    private const float Attack_Animation_Duration = 0.5f;

    private StateMachine _sm;
    private MovementState _movementState;
    private DeathState _deathState;
    private ZombieAttackState _zombieAttackState;

    private CharacterPlayerController _nearestPlayer;
    private List<float> _distances;
    private bool _canAttack = true;
    private bool _flipX;
    private bool _currentFlipX;

    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Animator _anim;
    [SerializeField] private PlayerPool _playerObjects;
    [SerializeField] private EnemyData _enemyData;
    [SerializeField] private Health _hp;
    [SerializeField] private SpriteRenderer _attack;
    [SerializeField] private SpriteRenderer _spriteObject;
    [SerializeField] private float _nearestPlayerDistance;

    public override void Spawned()
    {
        _sm = new StateMachine();
        _movementState = new MovementState(_anim, _enemyData.MovingSpeed, gameObject.GetComponent<Rigidbody2D>(), transform);
        _deathState = new DeathState();
        _zombieAttackState = new ZombieAttackState(_enemyData, _sm, _movementState);
        _hp.SetHP(_enemyData.HP);
        _sm.Initialize(_movementState);
        StartCoroutine(NearestPlayer());
    }
    public override void Activate() 
    {
        
    }
    public override void FixedUpdateNetwork()
    {
        _sm.CurrentState.UpdateSM(_nearestPlayer);
        
        if(_nearestPlayerDistance < _enemyData.AttackDistance&& _canAttack)
        {
            _canAttack = false;

            AttackBehavior();
            StartCoroutine(WaitBetweenAttack());
            RPC_AttackAnimation();
        }
        LookAtPlayer();
    }
    public void LookAtPlayer()
    {
        if (_nearestPlayer.transform.position.x > Object.transform.position.x)
        {
            _currentFlipX = false;
            if (_currentFlipX != _flipX)
            {
                _flipX = false;
                _spriteObject.transform.localRotation = Quaternion.Euler(Angle0, Angle0, Angle0);
            }
        }
        else if (_nearestPlayer.transform.position.x < Object.transform.position.x)
        {
            _currentFlipX = true;
            if (_currentFlipX != _flipX)
            {
                _flipX = true;
                _spriteObject.transform.localRotation = Quaternion.Euler(Angle0, Angle180, Angle0);
            }
        }
    }
    [Rpc]
    public void RPC_Rotate()
    {
        _spriteRenderer.flipX = _flipX;
    }
    [Rpc]
    public void RPC_AttackAnimation()
    {
        _attack.gameObject.SetActive(true);
        StartCoroutine(CloseAnimation());

    }
    IEnumerator CloseAnimation()
    {
        yield return new WaitForSeconds(Attack_Animation_Duration);
        _attack.gameObject.SetActive(false);
    }
    public override void MoveBehavior() 
    {
        _sm.ChangeState(_movementState);
    }
    public override void AttackBehavior() 
    {
        _sm.ChangeState(_zombieAttackState);
    }
    public override void Destroy() 
    {
        _sm.ChangeState(_deathState);
    }
    public void FoundNearestPlayer()
    {
        _distances = new List<float>();
        if (_playerObjects.players != null)
        {

            for (int i = 0; i <= _playerObjects.players.Count - 1; i++)
            {
                if (_playerObjects.players[i] == null)
                {
                    _playerObjects.players.RemoveAt(i);
                }
                else
                {
                    float distanceToPlayer = Vector3.Distance(gameObject.transform.position, _playerObjects.players[i].transform.position);
                    _distances.Add(distanceToPlayer);
                }
            }
            _nearestPlayerDistance = Mathf.Min(_distances.ToArray());
            int minDistanceIndex = _distances.IndexOf(_nearestPlayerDistance);
            if (minDistanceIndex >= 0)
                _nearestPlayer = _playerObjects.players[minDistanceIndex].GetComponent<CharacterPlayerController>(); ;
        }
    }
    IEnumerator NearestPlayer()
    {
        while (_playerObjects.players.Count > 0)
        {
            FoundNearestPlayer();
            yield return new WaitForSeconds(1f);
        }
    }
    IEnumerator WaitBetweenAttack()
    {
        yield return new WaitForSeconds(_enemyData.TimeBetweenAttacks);
        _canAttack = true;
    }
}
