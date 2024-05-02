using Fusion;
using UnityEngine;

public class CharacterShootController : NetworkBehaviour
{
    ///Const fields
    private const float ANGLEREFLECTEDCONSTANT = 2;
    private const float ANGLE0 = 0;
    private const float ANGLE90 = 90;
    private const float ANGLE180 = 180;
    ///

    private float _angleReflected;
    public GameObject player;
    public GameObject weapon;
    
    
    public WeaponController _weapon;


    
    /// ///////////////////////
    [Networked] private TickTimer delay { get; set; }
    
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
        if (Runner.TryGetInputForPlayer<CharacterInput>(Object.InputAuthority, out var input))
        {
            WeaponMove(input);
            if (HasStateAuthority)
            {
                if (input.Shoot)
                    _weapon.Shoot(input);
            }
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
}
