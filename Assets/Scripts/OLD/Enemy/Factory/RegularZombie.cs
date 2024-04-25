using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegularZombie : Enemy
{
    //StateMashine//
    private StateMachine _sm;
    private MovementState _movementState;
    public DeathState _deathState;
    public ZombieAttackState _zombieAttackState;
    //GameLogic//
    public Animator anim;
    public PlayerPool playerObjects;
    public EnemyData enemyData;
    private List<float> _distances;
    private GameObject _nearestPlayer;
    private float _nearestPlayerDistance;

    //private void Awake()
    //{
    //    _sm = new StateMachine();
    //    _movementState = new MovementState(this, _enemyData.MovingSpeed);
    //    _deathState = new DeathState(this);
    //    _zombieAttackState = new ZombieAttackState();
    //}
    public override void Spawned()
    {
        _sm = new StateMachine();
        _movementState = new MovementState(gameObject, enemyData.MovingSpeed);
        _deathState = new DeathState(gameObject);
        _zombieAttackState = new ZombieAttackState();
        _sm.Initialize(_movementState);
        StartCoroutine(NearestPlayer());
    }
    public override void Activate() 
    {
        
    }
    public override void FixedUpdateNetwork()
    {
        _sm.CurrentState.Update(_nearestPlayer);
        //if(nearestPlayerDistance< enemyData.AttackDistance)
        //{
        //    AttackBehavior();
        //}   
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
        foreach (GameObject player in playerObjects.players)
        {
            float distanceToPlayer = Vector3.Distance(gameObject.transform.position, player.transform.position);
            _distances.Add(distanceToPlayer);
        }
        _nearestPlayerDistance = Mathf.Min(_distances.ToArray());
        int minDistanceIndex = _distances.IndexOf(_nearestPlayerDistance);
        _nearestPlayer = playerObjects.players[minDistanceIndex];
    }
    IEnumerator NearestPlayer()
    {
        while (playerObjects.players.Count > 0)
        {
            FoundNearestPlayer();
            yield return new WaitForSeconds(1f);
        }
    }
}
