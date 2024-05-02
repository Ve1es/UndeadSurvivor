
using UnityEngine;

public class ZombieAttackState : IEnemyState
{
    private float _attackDamage;
    private StateMachine _stateMachine;
    private MovementState _movementState;
    public ZombieAttackState(EnemyData _enemyData, StateMachine stateMachine, MovementState movementState)
    {
        _attackDamage = _enemyData.Damage;
        _stateMachine = stateMachine;
        _movementState = movementState;
    }

    public void Enter()
    {

    }

    public void Exit()
    {

    }
    public void UpdateSM(GameObject _player)
    {
        _player.GetComponent<Health>().ReduceHP(_attackDamage);
        _stateMachine.ChangeState(_movementState);
    }
}
