
using UnityEngine;

public class PCInputStrategy : IInputStrategy
{
    public CharacterInput ProcessInput(CharacterInput input, JoystickMove joystickMove, JoystickWeapon joystickWeapon)
    {
        input.MoveHorizontalInput = Input.GetAxis("Horizontal");
        input.MoveVerticalInput = Input.GetAxis("Vertical");

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
