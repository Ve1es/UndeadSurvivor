using Fusion;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawner : NetworkBehaviour, IPlayerJoined, IPlayerLeft
{
    private const int PLAYER_COUNT = 2;
    private GameStateController _gameStateController = null;
    private List<int> weaponNumberList;
    [SerializeField] private NetworkPrefabRef[] _characterNetworkPrefabs;
    [SerializeField] private PlayerPool _playerPool;
    [SerializeField] private int _weaponsCount;
    [SerializeField] private GameObject[] _spawnPoints;
    [SerializeField] private List<WeaponData> weaponList;
    public Dictionary<PlayerRef, int> _selectedCharacters = new Dictionary<PlayerRef, int>();

    public void AddPlayerCharacter(int characterNumber, PlayerRef player)
    {
        if (_selectedCharacters.ContainsKey(player))
        {
            _selectedCharacters[player] = characterNumber;
        }
        else
        {
            _selectedCharacters.Add(player, characterNumber);
        }
    }

    public override void Spawned()
    {
        if (Object.HasStateAuthority == false) return;
        weaponNumberList = new List<int>();
        SetWeaponNumbers();
    }
    private void SetWeaponNumbers()
    {
        for (int i = 0; i < _weaponsCount; i++)
        {
            weaponNumberList.Add(i);
        }
    }

    public void StartCharacterSpawner(GameStateController gameStateController)
    {
        if (HasStateAuthority)
        {
            _gameStateController = gameStateController;
            foreach (var player in Runner.ActivePlayers)
            {
                SpawnCharacter(player);
            }
        }
    }

    public void PlayerJoined(PlayerRef player) { }

    private void SpawnCharacter(PlayerRef player)
    {
        int index = player.PlayerId % PLAYER_COUNT;
        var spawnPosition = _spawnPoints[index].transform.position;
        int character = _selectedCharacters[player];
        var playerObject = Runner.Spawn(_characterNetworkPrefabs[character], spawnPosition, Quaternion.identity, player);
        Runner.SetPlayerObject(player, playerObject);
        int weaponIndex = Random.Range(0, weaponList.Count - 1);
        playerObject.GetComponent<WeaponController>().WeaponNumber = weaponNumberList[weaponIndex];
        weaponNumberList.RemoveAt(weaponIndex);
    }

    public void PlayerLeft(PlayerRef player)
    {
        DespawnCharacter(player);
    }

    private void DespawnCharacter(PlayerRef player)
    {
        if (Runner.TryGetPlayerObject(player, out var playerNetworkObject))
        {
            Runner.Despawn(playerNetworkObject);
        }

        Runner.SetPlayerObject(player, null);
    }
}
