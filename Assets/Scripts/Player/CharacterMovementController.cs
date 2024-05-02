using Fusion;
using UnityEngine;

public class CharacterMovementController : NetworkBehaviour
{
    private float _movementSpeed;

    private Rigidbody2D _rigidbody;
    [SerializeField] private Animator _anim;
    private bool _isRun = false;

    private CharacterPlayerController _characterController = null;

    public override void Spawned()
    {
        // --- Host & Client
        // Set the local runtime references.
        _rigidbody = GetComponent<Rigidbody2D>();
        _characterController = GetComponent<CharacterPlayerController>();

        // --- Host
        // The Game Session SPECIFIC settings are initialized
        if (Object.HasStateAuthority == false) return;

    }

    public override void FixedUpdateNetwork()
    {
        if (Runner.TryGetInputForPlayer<CharacterInput>(Object.InputAuthority, out var input))
        {
            Move(input);
        }
        if((_rigidbody.velocity.x!=0|| _rigidbody.velocity.y != 0) && !_isRun)
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
    }
    public void SetSpeed(float speed)
    {
        _movementSpeed = speed;
    }
}
