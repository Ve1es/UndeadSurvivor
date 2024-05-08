using Fusion;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : NetworkBehaviour
{
    private const string ENEMY_TAG = "Enemy";
    private const float Bullet_Speed = 10;
    
    private int _currentWeapon = 0;
    private float _nextFireTime = 0;

    [Networked] private int _ammo { get; set; }

    [Networked] [SerializeField] private int _maxAmmo { get; set; }

    [SerializeField] private NetworkPrefabRef _bulletPrefab = NetworkPrefabRef.Empty;
    [SerializeField] private SpriteRenderer _playersWeaponSprite;
    [SerializeField] private List<WeaponData> _weaponList;

    public Transform SpawnBulletPoint;

    [Networked] public bool SpawnedProjectile { get; set; }
    [Networked] public int WeaponNumber { get; set; }

    public void AppointWeapon()
    {
        _currentWeapon = WeaponNumber;
        _playersWeaponSprite.sprite = _weaponList[WeaponNumber].WeaponSprite;
        _ammo = _weaponList[WeaponNumber].Ammo;
        _maxAmmo = _weaponList[WeaponNumber].Ammo;
    }
    public void AddAmmo()
    {
        _ammo = _maxAmmo;
    }
    public int GetAmmo()
    {
        return _ammo;
    }
    public override void Spawned()
    {
        AppointWeapon();
    }
    public override void FixedUpdateNetwork()
    {
        if(WeaponNumber != _currentWeapon)
        {
            AppointWeapon();
        }
        if (_nextFireTime > 0)
            _nextFireTime -= Runner.DeltaTime;
    }
    public void Shoot(CharacterInput input)
    {
        if (_nextFireTime <= 0 && _ammo > 0)
        {
            CreateBullet();
            SpawnedProjectile = !SpawnedProjectile;
            _nextFireTime = _weaponList[WeaponNumber].TimeBetweenShots;
            _ammo--;
        }
    }
    public void CreateBullet()
    {
        if (HasStateAuthority)
        {
            if (_weaponList[WeaponNumber].BulletPerShot <= 1)
            {
                Runner.Spawn(_bulletPrefab,
                 SpawnBulletPoint.position,
                 SpawnBulletPoint.rotation,
                 Object.InputAuthority,
                 (runner, o) =>
                 {
                     o.GetComponent<Bullet>().Init(_weaponList[WeaponNumber].FlightDistance, Bullet_Speed);
                     o.GetComponent<DealDamage>().SetDamage(_weaponList[WeaponNumber].Damage);
                     o.GetComponent<DealDamage>().SetGoalTag(ENEMY_TAG);
                 });
            }
            else
            {
                for (int i = 0; i < _weaponList[WeaponNumber].BulletPerShot; i++)
                {
                    float deviationAngle = Random.Range(-_weaponList[WeaponNumber].Spread, _weaponList[WeaponNumber].Spread);
                    Vector3 deviation = Quaternion.Euler(0, 0, deviationAngle) * SpawnBulletPoint.right;

                    Runner.Spawn(_bulletPrefab,
                    SpawnBulletPoint.position,
                    SpawnBulletPoint.rotation,
                    Object.InputAuthority,
                    (runner, o) =>
                    {
                        o.GetComponent<Bullet>().Init(deviation, _weaponList[WeaponNumber].FlightDistance, Bullet_Speed);
                        o.GetComponent<DealDamage>().SetDamage(_weaponList[WeaponNumber].Damage);
                        o.GetComponent<DealDamage>().SetGoalTag(ENEMY_TAG);
                    });
                }
            }
        }
    }
}
