using Fusion;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : NetworkBehaviour
{
    private const string ENEMY_TAG = "Enemy";
    private const float BULLET_SPEED = 10; 
    private int _currentWeapon=0;
    private float _nextFireTime = 0;
    [Networked] private int _ammo { get; set; }
    [Networked] [SerializeField] private int _maxAmmo { get; set; }
    [SerializeField] private NetworkPrefabRef _bulletPrefab = NetworkPrefabRef.Empty;
    [SerializeField] private SpriteRenderer _playersWeaponSprite;
    [SerializeField] private List<WeaponData> _weaponList;
    public Transform spawnBulletPoint;
    [Networked] public bool spawnedProjectile { get; set; }
    [Networked] public int weaponNumber { get; set; }

    public void AppointWeapon()
    {
        _currentWeapon = weaponNumber;
        _playersWeaponSprite.sprite = _weaponList[weaponNumber].WeaponSprite;
        _ammo = _weaponList[weaponNumber].Ammo;
        _maxAmmo = _weaponList[weaponNumber].Ammo;
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
        if(weaponNumber!= _currentWeapon)
        {
            AppointWeapon();
        }
        if (_nextFireTime > 0)
            _nextFireTime -= Runner.DeltaTime;
    }
    public void Shoot(CharacterInput input)
    {
        if (_nextFireTime <= 0&&_ammo>0)
        {
            CreateBullet();
            spawnedProjectile = !spawnedProjectile;
            _nextFireTime = _weaponList[weaponNumber].TimeBetweenShots;
            _ammo--;
        }
    }
    public void CreateBullet()
    {
        if (HasStateAuthority)
        {
            if (_weaponList[weaponNumber].BulletPerShot <= 1)
            {
                Runner.Spawn(_bulletPrefab,
                 spawnBulletPoint.position,
                 spawnBulletPoint.rotation,
                 Object.InputAuthority,
                 (runner, o) =>
                 {
                     o.GetComponent<Bullet>().Init(_weaponList[weaponNumber].FlightDistance, BULLET_SPEED);
                     o.GetComponent<DealDamage>().SetDamage(_weaponList[weaponNumber].Damage);
                     o.GetComponent<DealDamage>().SetGoalTag(ENEMY_TAG);
                 });
            }
            else
            {
                for (int i = 0; i < _weaponList[weaponNumber].BulletPerShot; i++)
                {
                    float deviationAngle = Random.Range(-_weaponList[weaponNumber].Spread, _weaponList[weaponNumber].Spread);
                    Vector3 deviation = Quaternion.Euler(0, 0, deviationAngle) * spawnBulletPoint.right;


                    Runner.Spawn(_bulletPrefab,
                    spawnBulletPoint.position,
                    spawnBulletPoint.rotation,
                    Object.InputAuthority,
                    (runner, o) =>
                    {
                        o.GetComponent<Bullet>().Init(deviation, _weaponList[weaponNumber].FlightDistance, BULLET_SPEED);
                        o.GetComponent<DealDamage>().SetDamage(_weaponList[weaponNumber].Damage);
                        o.GetComponent<DealDamage>().SetGoalTag(ENEMY_TAG);
                    });
                }
            }
        }
    }
}
