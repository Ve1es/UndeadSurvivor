using Fusion;
using UnityEngine;

public class CharacterMovementController : NetworkBehaviour
{
    private const float ANGLE_REFLECTED_CONSTANT = 2;
    private const float ANGLE0 = 0;
    private const float ANGLE90 = 90;
    private const float ANGLE180 = 180;
    private float _movementSpeed;
    private Rigidbody2D _rigidbody;
    private bool _isRun = false;
    private float _angleReflected;
    public GameObject player;
    public GameObject weapon;
    [SerializeField] private Animator _anim;

    public override void Spawned()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        if (Object.HasStateAuthority == false) return;
    }

    public override void FixedUpdateNetwork()
    {
        if (Runner.TryGetInputForPlayer<CharacterInput>(Object.InputAuthority, out var input))
        {
            Move(input);
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
        _anim.SetBool("Run", isRun);
    }


    private void Move(CharacterInput input)
    {
        Vector3 movement = new Vector3(input.MoveHorizontalInput * _movementSpeed, input.MoveVerticalInput * _movementSpeed, 0f);
        _rigidbody.velocity = movement;
        Debug.Log(input.Shoot);
        if (!input.Shoot)
        {
            float angleRadians = Mathf.Atan2(input.MoveVerticalInput, input.MoveHorizontalInput);
            float angleDegrees = angleRadians * Mathf.Rad2Deg;
            _angleReflected = angleDegrees - (angleDegrees - ANGLE90) * ANGLE_REFLECTED_CONSTANT;
            //Debug.Log(angleDegrees);
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
    public void SetSpeed(float speed)
    {
        _movementSpeed = speed;
    }
}
