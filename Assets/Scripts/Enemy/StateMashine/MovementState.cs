using UnityEngine;

public class MovementState : IEnemyState
{
    private GameObject _enemy;
    private float _moveSpeed;
    private Rigidbody2D _rb;
    public MovementState(GameObject enemy, float speed)
    {
        _enemy = enemy;
        _moveSpeed = speed;
    }
    public void Enter()
    {
        _rb = _enemy.GetComponent<Rigidbody2D>();
        _enemy.GetComponent<Animator>().SetBool("Run", true);
    }

    public void Exit()
    {
        _enemy.GetComponent<Animator>().SetBool("Run", false);
    }

    public void UpdateSM(GameObject _player)
    {
        if (_player != null)
        {
            Vector3 direction = (_player.transform.position - _enemy.transform.position).normalized;
            _rb.velocity = direction * _moveSpeed;
        }
    }
}
