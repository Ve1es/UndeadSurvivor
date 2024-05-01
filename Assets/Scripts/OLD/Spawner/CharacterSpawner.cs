using Fusion;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawner : NetworkBehaviour, IPlayerJoined, IPlayerLeft
{
    // References to the NetworkObject prefab to be used for the players' spaceships.
    [SerializeField] private NetworkPrefabRef _characterNetworkPrefab = NetworkPrefabRef.Empty;
    [SerializeField] private PlayerPool _playerPool;
    private bool _gameIsReady = false;
    private GameStateController _gameStateController = null;
    private const int PLAYER_COUNT = 2;
    //private int _issuedWeapon = -1;
    [SerializeField] private int _weaponsCount;
    [SerializeField] private GameObject[] _spawnPoints;
    [SerializeField] private List<WeaponData> weaponList;
    private List<int> weaponNumberList;

    public override void Spawned()
    {
        if (Object.HasStateAuthority == false) return;
        weaponNumberList = new List<int>();
        SetWeaponNumbers();
    }
    private void SetWeaponNumbers()
    {
        for(int i = 0; i< _weaponsCount; i++)
        {
            weaponNumberList.Add(i);
        }
    }

    // The spawner is started when the GameStateController switches to GameState.Running.
    public void StartCharacterSpawner(GameStateController gameStateController)
    {
        _gameIsReady = true;
        _gameStateController = gameStateController;
        foreach (var player in Runner.ActivePlayers)
        {
            SpawnCharacter(player);           
        }
    }

    public void PlayerJoined(PlayerRef player)
    {
        if (_gameIsReady == false) return;
        SpawnCharacter(player);
    }

    // Spawns a spaceship for a player.
    // The spawn point is chosen in the _spawnPoints array using the implicit playerRef to int conversion 
    private void SpawnCharacter(PlayerRef player)
    {
        // Modulo is used in case there are more players than spawn points.
        int index = player.PlayerId % PLAYER_COUNT;
        var spawnPosition = _spawnPoints[index].transform.position;


        var playerObject = Runner.Spawn(_characterNetworkPrefab, spawnPosition, Quaternion.identity, player);
        // Set Player Object to facilitate access across systems.
        Runner.SetPlayerObject(player, playerObject);
        
        // Add the new spaceship to the players to be tracked for the game end check.
        _gameStateController.TrackNewPlayer(playerObject.GetComponent<PlayerDataNetworked>().Id);
        int weaponIndex = Random.Range(0, weaponList.Count - 1);
        playerObject.GetComponent<WeaponController>().weaponNumber = weaponNumberList[weaponIndex];
        weaponNumberList.RemoveAt(weaponIndex);
        _playerPool.RegisterPlayer(playerObject.gameObject);

        //playerObject.GetComponent<WeaponController>().AppointWeapon();
        //playerObject.GetComponent<WeaponController>()._playersWeaponSprite.sprite = weaponList[weaponIndex].WeaponSprite;

    }

    // Despawns the spaceship associated with a player when their client leaves the game session.
    public void PlayerLeft(PlayerRef player)
    {
        DespawnCharacter(player);
    }

    private void DespawnCharacter(PlayerRef player)
    {
        if (Runner.TryGetPlayerObject(player, out var spaceshipNetworkObject))
        {
            Runner.Despawn(spaceshipNetworkObject);
        }

       // Runner.SetPlayerObject(player, null);
    }
}
