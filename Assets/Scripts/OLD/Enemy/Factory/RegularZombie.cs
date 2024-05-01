using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegularZombie : Enemy
{
    //StateMashine//
    private StateMachine _sm;
    private MovementState _movementState;
    private DeathState _deathState;
    private ZombieAttackState _zombieAttackState;
    //GameLogic//
    [SerializeField] private Animator _anim;
    [SerializeField] private PlayerPool _playerObjects;
    [SerializeField] private EnemyData _enemyData;
    [SerializeField] private Health _hp;

    private List<float> _distances;
    private GameObject _nearestPlayer;
    private float _nearestPlayerDistance;
    private bool _canAttack=true;
    public override void Spawned()
    {
        _sm = new StateMachine();
        _movementState = new MovementState(gameObject, _enemyData.MovingSpeed);
        _deathState = new DeathState(gameObject);
        _zombieAttackState = new ZombieAttackState(_enemyData, _sm, _movementState);
        _hp.SetHP(_enemyData.HP);
        _sm.Initialize(_movementState);

        StartCoroutine(NearestPlayer());
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
       if(_canAttack && other.gameObject.CompareTag("Player"))
        {
            _canAttack = false;
            _nearestPlayer = other.gameObject;
            
            AttackBehavior();
            StartCoroutine(WaitForSecondsCoroutine());
        }
    }
    public override void Activate() 
    {
        
    }
    public override void FixedUpdateNetwork()
    {
        _sm.CurrentState.UpdateSM(_nearestPlayer);  
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
            for (int i = _playerObjects.players.Count - 1; i >= 0; i--)
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
                _nearestPlayer = _playerObjects.players[minDistanceIndex];
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
    IEnumerator WaitForSecondsCoroutine()
    {
        yield return new WaitForSeconds(_enemyData.TimeBetweenAttacks);
        _canAttack = true;
    }
}
