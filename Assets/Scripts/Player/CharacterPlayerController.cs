using Fusion;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPlayerController : NetworkBehaviour
{
    [SerializeField] private PlayerPool _playerPool;
    private float _playerId;


    // Local Runtime references
    //private ChangeDetector _changeDetector;
    //private Rigidbody _rigidbody = null;
    private PlayerDataNetworked _playerDataNetworked = null;
    [SerializeField] private GameObject _playerSprite;
    [SerializeField] private Animator _playerAnimator;

    [SerializeField] private PlayerStats _playerStats;
    [SerializeField] private CharacterList _characterList;
    // Game Session SPECIFIC Settings

    [Networked] private NetworkBool _isAlive { get; set; }



    public override void Spawned()
    {
        // --- Host & Client
        // Set the local runtime references.

        //_rigidbody = GetComponent<Rigidbody>();
        _playerDataNetworked = GetComponent<PlayerDataNetworked>();
        //_changeDetector = GetChangeDetector(ChangeDetector.Source.SimulationState);

        // --- Host
        // The Game Session SPECIFIC settings are initialized
        if (Object.HasStateAuthority == false) return;
        _isAlive = true;

        //_playerPool.RegisterPlayer(gameObject);
        gameObject.GetComponent<Health>().SetHP(_playerStats.HP);
        gameObject.GetComponent<CharacterMovementController>().SetSpeed(_playerStats.MovingSpeed);
        if(HasInputAuthority)
        Rpc_SetCharacter();

    }
    [Rpc]
    private void Rpc_SetCharacter()
    {
        _playerSprite.GetComponent<SpriteRenderer>().sprite = _characterList.GetSprite(_playerDataNetworked.playerCharacterNumber);
        _playerAnimator.runtimeAnimatorController = _characterList.GetAnimator(_playerDataNetworked.playerCharacterNumber);
    }
    public float GetPlayerId()
    {
        return _playerId;
    }
}
