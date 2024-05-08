using Fusion;
using UnityEngine;

public class CharacterMovementController : NetworkBehaviour
{
    private const float Angle_Reflected_Constant = 2;
    private const float Angle0 = 0;
    private const float Angle90 = 90;
    private const float Angle180 = 180;

    private static readonly int Run = Animator.StringToHash("Run");
    
    private Rigidbody2D _rigidbody;
    private bool _isRun = false;
    private float _movementSpeed;
    private float _angleReflected;

    [SerializeField] private Animator _anim;

    public Transform Player;
    public Transform Weapon;
    

    public override void Spawned()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        if (Object.HasStateAuthority == false) return;
    }

    public override void FixedUpdateNetwork()
    {
        if (Runner.TryGetInputForPlayer<CharacterInput>(Object.InputAuthority, out var input))
        {
            if (HasStateAuthority)
            {
                
                    Move(input);
                
            }
        }
        if ((_rigidbody.velocity.x != 0 || _rigidbody.velocity.y != 0) && !_isRun)
        {
            _isRun = true;
            RPC_ChangeMoveAnim(_isRun);
        }
        if (_rigidbody.velocity.x == 0 && _rigidbody.velocity.y == 0 && _isRun)
        {
            _isRun = false;
            RPC_ChangeMoveAnim(_isRun);
        }
    }

    [Rpc]
    public void RPC_ChangeMoveAnim(bool isRun)
    {
        _anim.SetBool(Run, isRun);
    }


    private void Move(CharacterInput input)
    {
        Vector3 movement = new Vector3(input.MoveHorizontalInput * _movementSpeed, input.MoveVerticalInput * _movementSpeed, 0f);
        _rigidbody.velocity = movement;
        if (!input.Shoot)
        {
            float angleRadians = Mathf.Atan2(input.MoveVerticalInput, input.MoveHorizontalInput);
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
    public void SetSpeed(float speed)
    {
        _movementSpeed = speed;
    }
}
