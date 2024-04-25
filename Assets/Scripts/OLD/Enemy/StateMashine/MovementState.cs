using UnityEngine;

public class MovementState : IEnemyState
{
    private GameObject _enemy;
    private float _moveSpeed;
    private Rigidbody2D _rb;
    public MovementState(GameObject enemy, float moveSpeed)
    {
        _enemy = enemy;
        _moveSpeed = moveSpeed;
    }
    public void Enter()
    {
        _rb = _enemy.GetComponent<Rigidbody2D>();
    }

    public void Exit()
    {

    }

    public void Update(GameObject _player)
    {
        if (_player != null)
        {
            Vector3 direction = (_player.transform.position - _enemy.transform.position).normalized;
            _rb.velocity = direction * _moveSpeed;
        }
    }
}
