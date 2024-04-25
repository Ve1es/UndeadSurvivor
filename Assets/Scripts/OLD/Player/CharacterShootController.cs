using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterShootController : NetworkBehaviour
{
    private float _nextFireTime = 0f;
    private float _angleReflected;
    private float ANGLEREFLECTEDCONSTANT = 2;
    private static float ANGLE0 = 0;
    private static float ANGLE90 = 90;
    private static float ANGLE180 = 180;
    public GameObject player;
    public GameObject weapon;
    public GameObject bulletPrefab;
    public Transform spawnBulletPoint;
    // public Rigidbody2D _weaponRigedbody;

    public float fireRate = 1f;
    /// ///////////////////////
    [Networked] private TickTimer delay { get; set; }
    [Networked] public bool spawnedProjectile { get; set; }
    /// //////////////////////////
    private CharacterPlayerController _characterController = null;

    public override void Spawned()
    {
        // --- Host & Client
        // Set the local runtime references.
        _characterController = GetComponent<CharacterPlayerController>();

        // --- Host
        // The Game Session SPECIFIC settings are initialized
        if (Object.HasStateAuthority == false) return;
    }

    public override void FixedUpdateNetwork()
    {
        if (_characterController.AcceptInput == false) return;
        FiringDelay();
        if (Runner.TryGetInputForPlayer<CharacterInput>(Object.InputAuthority, out var input))
        {
            WeaponMove(input);
            if (input.Shoot && _nextFireTime <= 0)
            Fire(input);
        }
    }

    private void WeaponMove(CharacterInput input)
    {
        float angleRadians = Mathf.Atan2(input.WeaponVerticalInput, input.WeaponHorizontalInput);
        float angleDegrees = angleRadians * Mathf.Rad2Deg;
        _angleReflected = angleDegrees - (angleDegrees - ANGLE90) * ANGLEREFLECTEDCONSTANT;

        if (angleDegrees > ANGLE90 || angleDegrees < -ANGLE90)
        {
            if (player.transform.localRotation.y != ANGLE180)
                player.transform.localRotation = Quaternion.Euler(ANGLE0, ANGLE180, ANGLE0);
            if (weapon.transform.localRotation.z != _angleReflected
                || weapon.transform.localRotation.y != ANGLE180)
            {
                weapon.transform.localRotation = Quaternion.Euler(ANGLE0, ANGLE180, _angleReflected);
            }
        }
        else
        {
            if (player.transform.localRotation.y != ANGLE0)
                player.transform.localRotation = Quaternion.Euler(ANGLE0, ANGLE0, ANGLE0);
            if (weapon.transform.localRotation.z != angleDegrees
                || weapon.transform.localRotation.y != ANGLE0)
            {
                weapon.transform.localRotation = Quaternion.Euler(ANGLE0, ANGLE0, angleDegrees);
            }
        }
    }
    private void FiringDelay()
    {
        if (_nextFireTime > 0)
            _nextFireTime -= Runner.DeltaTime;
    }
    private void Fire(CharacterInput input)
    {
        //  if (_nextFireTime <= 0&&input.Shoot)
        //  {
        //      GameObject projectile = Instantiate(bulletPrefab, spawnBulletPoint.position, spawnBulletPoint.rotation);
        //      Runner.Spawn(bulletPrefab,
        //spawnBulletPoint.position, spawnBulletPoint.rotation,
        //Object.InputAuthority);
        //      _nextFireTime = fireRate;
        //  }
        delay = TickTimer.CreateFromSeconds(Runner, 0.5f);
        Runner.Spawn(bulletPrefab,
          spawnBulletPoint.position,
          spawnBulletPoint.rotation,
          Object.InputAuthority,
          (runner, o) =>
          {
              // Initialize the Ball before synchronizing it
              o.GetComponent<Bullet>().Init();
          });
        //Runner.Spawn(bulletPrefab, spawnBulletPoint.position, Quaternion.identity);
        spawnedProjectile = !spawnedProjectile;
        _nextFireTime = fireRate;
    }
}
