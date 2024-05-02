using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Enemy
{
    //StateMashine//
    private StateMachine _sm;
    private MovementState _movementState;
    private DeathState _deathState;
    private SkeletonAttackState _skeletonAttackState;
    //GameLogic//
    
    [SerializeField] private PlayerPool _playerObjects;
    [SerializeField] private EnemyData _enemyData;
    [SerializeField] private Health _hp;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Transform _bulletSpawn;

    private List<float> _distances;
    private GameObject _nearestPlayer;
    private float _nearestPlayerDistance;
    public Animator _anim;

    public override void Spawned()
    {
        _sm = new StateMachine();
        _movementState = new MovementState(gameObject, _enemyData.MovingSpeed);
        _deathState = new DeathState(gameObject);
        _skeletonAttackState = new SkeletonAttackState(gameObject, _enemyData, _bullet, _bulletSpawn, gameObject.GetComponent<Enemy>());
        _sm.Initialize(_movementState);
        _hp.SetHP(_enemyData.HP);
        StartCoroutine(NearestPlayer());
    }
    public override void Activate()
    {

    }
    
    public override void FixedUpdateNetwork()
    {
        if(_nearestPlayerDistance<=_enemyData.AttackDistance && _sm.CurrentState is MovementState)
        {
            AttackBehavior();
        }
        if(_nearestPlayerDistance > _enemyData.AttackDistance && _sm.CurrentState is SkeletonAttackState)
        {
            MoveBehavior();
        }
        _sm.CurrentState.UpdateSM(_nearestPlayer);
    }
    public override void MoveBehavior()
    {
        _sm.ChangeState(_movementState);
    }
    public override void AttackBehavior()
    {
        _sm.ChangeState(_skeletonAttackState);
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

            for (int i = 0; i<= _playerObjects.players.Count - 1; i++)
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
}
