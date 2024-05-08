using UnityEngine;

public class MovementState : IEnemyState
{
    private static readonly int Run = Animator.StringToHash("Run");
    private Animator _enemyAnimator;
    private Rigidbody2D _rb;
    private Transform _enemyTransform;
    private float _moveSpeed;
    public MovementState(Animator enemyAnimator, float speed, Rigidbody2D enemyRigidbody, Transform enemyTransform)
    {
        _enemyAnimator = enemyAnimator;
        _moveSpeed = speed;
        _rb = enemyRigidbody;
        _enemyTransform = enemyTransform;
    }
    public void Enter()
    {
        _enemyAnimator.GetComponent<Animator>().SetBool(Run, true);
    }

    public void Exit()
    {
        _enemyAnimator.GetComponent<Animator>().SetBool(Run, false);
    }

    public void UpdateSM(CharacterPlayerController _player)
    {
        if (_player != null)
        {
            Vector3 direction = (_player.transform.position - _enemyTransform.position).normalized;
            _rb.velocity = direction * _moveSpeed;
        }
    }
}
