using ExitGames.Client.Photon.StructWrapping;
using Fusion;
using Fusion.Addons.Physics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovementController : NetworkBehaviour
{  // Game Session AGNOSTIC Settings
    [SerializeField] private float _movementSpeed = 20f;

    private Rigidbody2D _rigidbody;

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
        if (_characterController.AcceptInput == false) return;
        if (Runner.TryGetInputForPlayer<CharacterInput>(Object.InputAuthority, out var input))
        {
            Move(input);
        }
    }
    private void Move(CharacterInput input)
    {
        Vector3 movement = new Vector3(input.MoveHorizontalInput * _movementSpeed, input.MoveVerticalInput * _movementSpeed, 0f);
        _rigidbody.velocity = movement;
    }
}
