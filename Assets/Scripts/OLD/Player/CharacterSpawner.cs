using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawner : NetworkBehaviour, IPlayerJoined, IPlayerLeft
{
    // References to the NetworkObject prefab to be used for the players' spaceships.
    [SerializeField] private NetworkPrefabRef _characterNetworkPrefab = NetworkPrefabRef.Empty;

    private bool _gameIsReady = false;
    private GameStateController _gameStateController = null;
    private const int _playerCount = 2;

    public GameObject[] _spawnPoints;

    public override void Spawned()
    {
        if (Object.HasStateAuthority == false) return;
        // Collect all spawn points in the scene.
        //_spawnPoints = FindObjectsOfType<SpawnPoint>();
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
        int index = player.PlayerId % _playerCount;
        var spawnPosition = _spawnPoints[index].transform.position;

        var playerObject = Runner.Spawn(_characterNetworkPrefab, spawnPosition, Quaternion.identity, player);
        // Set Player Object to facilitate access across systems.
        Runner.SetPlayerObject(player, playerObject);

        // Add the new spaceship to the players to be tracked for the game end check.
        _gameStateController.TrackNewPlayer(playerObject.GetComponent<PlayerDataNetworked>().Id);
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

        // Reset Player Object
        Runner.SetPlayerObject(player, null);
    }
}
