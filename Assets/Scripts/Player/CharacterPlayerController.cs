using Fusion;
using UnityEngine;

public class CharacterPlayerController : NetworkBehaviour
{
    private float _playerId;

    [SerializeField] private PlayerPool _playerPool;
    [SerializeField] private Animator _playerAnimator;
    [SerializeField] private PlayerCamera _playerCamera;
    [SerializeField] private PlayerStats _playerStats;
    [SerializeField] private CharacterList _characterList;

    [SerializeField] public Health Health;
    [SerializeField] public string CharacterInput;
    
    
    public override void Spawned()
    {
        gameObject.GetComponent<Health>().SetHP(_playerStats.HP);
        gameObject.GetComponent<CharacterMovementController>().SetSpeed(_playerStats.MovingSpeed);
        RPC_AddPlayerInList();
    }
    [Rpc]
    public void RPC_AddPlayerInList()
    {
        _playerPool.RegisterPlayer(gameObject);
        _playerPool.RegisterPlayerInputNumber(Object.InputAuthority.ToString());
    }
    public override void FixedUpdateNetwork()
    {
        CharacterInput = Object.InputAuthority.ToString();
    }
    public float GetPlayerId()
    {
        return _playerId;
    }
}
