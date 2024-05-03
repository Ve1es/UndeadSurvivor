using Fusion;
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
    [SerializeField] private PlayerCamera _playerCamera;

    [SerializeField] private PlayerStats _playerStats;
    [SerializeField] private CharacterList _characterList;
    // Game Session SPECIFIC Settings

    [Networked] private NetworkBool _isAlive { get; set; }



    public override void Spawned()
    {
        // --- Host & Client
        // Set the local runtime references.
        _playerDataNetworked = GetComponent<PlayerDataNetworked>();
        gameObject.GetComponent<Health>().SetHP(_playerStats.HP);
        gameObject.GetComponent<CharacterMovementController>().SetSpeed(_playerStats.MovingSpeed);
        //_rigidbody = GetComponent<Rigidbody>();

    }

    public float GetPlayerId()
    {
        return _playerId;
    }
}
