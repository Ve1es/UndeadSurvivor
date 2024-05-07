using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumpedZombie : Enemy
{
    private StateMachine _sm;
    private MovementState _movementState;
    private List<float> _distances;
    private GameObject _nearestPlayer;
    private float _nearestPlayerDistance;
    [SerializeField] private Animator anim;
    [SerializeField] private PlayerPool playerObjects;
    [SerializeField] private EnemyData _enemyData;
    public DeathState _deathState;
    public ZombieAttackState _zombieAttackState;
    public override void Spawned()
    {
        _sm = new StateMachine();
        _movementState = new MovementState(gameObject, _enemyData.MovingSpeed);
        _deathState = new DeathState(gameObject);
        _zombieAttackState = new ZombieAttackState(_enemyData, _sm, _movementState);
        _sm.Initialize(_movementState);
        StartCoroutine(NearestPlayer());
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
