using Fusion;
using UnityEngine;

public class CharacterShootController : NetworkBehaviour
{
    private const float Angle_Reflected_Constant = 2;
    private const float Angle0 = 0;
    private const float Angle90 = 90;
    private const float Angle180 = 180;

    private float _angleReflected;

    public Transform Player;
    public Transform Weapon; 
    public WeaponController _weapon;
    
    public override void Spawned()
    {
        if (Object.HasStateAuthority == false) return;
    }

    public override void FixedUpdateNetwork()
    {
        if (Runner.TryGetInputForPlayer<CharacterInput>(Object.InputAuthority, out var input))
        {
           
            if (HasStateAuthority)
            {
                if (input.Shoot)
                {
                    WeaponMove(input);
                    _weapon.Shoot(input);
                }
            }
        }
    }

    private void WeaponMove(CharacterInput input)
    {
        float angleRadians = Mathf.Atan2(input.WeaponVerticalInput, input.WeaponHorizontalInput);
        float angleDegrees = angleRadians * Mathf.Rad2Deg;
        _angleReflected = angleDegrees - (angleDegrees - Angle90) * Angle_Reflected_Constant;
        if (angleDegrees > Angle90 || angleDegrees < -Angle90)
        {
            if (Player.localRotation.y != Angle180)
                Player.localRotation = Quaternion.Euler(Angle0, Angle180, Angle0);
            if (Weapon.localRotation.z != _angleReflected
                || Weapon.localRotation.y != Angle180)
            {
                Weapon.localRotation = Quaternion.Euler(Angle0, Angle180, _angleReflected);
            }
        }
        else
        {
            if (Player.localRotation.y != Angle0)
                Player.localRotation = Quaternion.Euler(Angle0, Angle0, Angle0);
            if (Weapon.localRotation.z != angleDegrees
                || Weapon.localRotation.y != Angle0)
            {
                Weapon.localRotation = Quaternion.Euler(Angle0, Angle0, angleDegrees);
            }
        }
    }
}
