using UnityEngine;
using System.Collections;

public class SkeletonAttackState : IEnemyState
{
    private const string Player_Tag = "Player";
    private Enemy _monstrController;
    private Transform _target;
    private Transform _himself;
    private Bullet _bullet;
    private Rigidbody2D _rb;
    private float _attackDamage;
    private float _timeBetweenAttack;
    private bool _canAttack;
    
    public SkeletonAttackState(Transform himself, EnemyData _enemyData, Bullet bullet, Transform bulletSpawnPoint, Enemy monstrController, Rigidbody2D _rigidbody)
    {
        _attackDamage = _enemyData.Damage;
        _bullet = bullet;
        _timeBetweenAttack = _enemyData.TimeBetweenAttacks;
        _himself = himself;
        _monstrController = monstrController;
        _rb = _rigidbody;
    }
    public void Enter()
    {
        _canAttack = true;
        _rb.velocity = Vector2.zero;
    }

    public void Exit()
    {

    }

    public void UpdateSM(CharacterPlayerController _player)
    {
        if (_canAttack)
        {
            _target = _player.transform;
            Shoot();
        }
    }

    private void Shoot()
    {
        if (_target != null)
        {
            Vector3 direction = _target.position - _himself.transform.position;
            direction.Normalize();
            _monstrController.Runner.Spawn(_bullet,
              _himself.transform.position,
              _himself.transform.rotation,
              _monstrController.Object.InputAuthority,
              (runner, o) =>
              {
                  o.GetComponent<Bullet>().Init(direction, 10, 4);
                  o.GetComponent<DealDamage>().SetDamage(_attackDamage);
                  o.GetComponent<DealDamage>().SetGoalTag(Player_Tag);
              });
            _canAttack = false;
            _monstrController.StartCoroutine(ShootDelay());
        }
    }
    IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(_timeBetweenAttack);

        _canAttack = true;
    }
}
