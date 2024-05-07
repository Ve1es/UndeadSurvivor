using UnityEngine;

public class MobileInputStrategy : IInputStrategy
{
    public CharacterInput ProcessInput(CharacterInput input, JoystickMove joystickMove, JoystickWeapon joystickWeapon)
    {
        if (joystickMove != null)
        {
            input.MoveHorizontalInput = joystickMove.Horizontal();
            input.MoveVerticalInput = joystickMove.Vertical();
        }
        if (joystickWeapon != null)
        {
            input.Shoot = joystickWeapon.Shoot();
            input.WeaponHorizontalInput = joystickWeapon.Horizontal();
            input.WeaponVerticalInput = joystickWeapon.Vertical();
        }
        return input;
    }
}
