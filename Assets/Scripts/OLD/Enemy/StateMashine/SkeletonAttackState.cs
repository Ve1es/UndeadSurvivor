using UnityEngine;
using System.Collections;

public class SkeletonAttackState : IEnemyState
{
    private Enemy _monstrController;
    private GameObject _target;
    private GameObject _bullet;
    private GameObject _himself;
    private Rigidbody2D _rb;
    private float _attackDamage;
    private float _timeBetweenAttack;
    private bool _canAttack;
    private const string PLAYER_TAG = "Player";
    
    public SkeletonAttackState(GameObject himself, EnemyData _enemyData, GameObject bullet, Transform bulletSpawnPoint, Enemy monstrController)
    {
        _attackDamage = _enemyData.Damage;
        _bullet = bullet;
        _timeBetweenAttack = _enemyData.TimeBetweenAttacks;
        _himself = himself;
        _monstrController = monstrController;
    }
    public void Enter()
    {
        _canAttack = true;
        _rb = _himself.GetComponent<Rigidbody2D>();
        _rb.velocity = Vector2.zero;
    }

    public void Exit()
    {

    }

    public void UpdateSM(GameObject _player)
    {
        if (_canAttack)
        {
            _target = _player;
            Shoot();
        }
    }

    private void Shoot()
    {
        if (_target != null)
        {
            Vector3 direction = _target.transform.position - _himself.transform.position;
            direction.Normalize();
            _monstrController.Runner.Spawn(_bullet,
              _himself.transform.position,
              _himself.transform.rotation,
              _monstrController.Object.InputAuthority,
              (runner, o) =>
              {
                  o.GetComponent<Bullet>().Init(direction, 10, 4);
                  o.GetComponent<DealDamage>().SetDamage(_attackDamage);
                  o.GetComponent<DealDamage>().SetGoalTag(PLAYER_TAG);
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
