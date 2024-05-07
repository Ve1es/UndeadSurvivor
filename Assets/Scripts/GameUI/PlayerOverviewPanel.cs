using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;
using Fusion;

public class PlayerOverviewPanel : MonoBehaviour
{
    private Dictionary<PlayerRef, TextMeshProUGUI>
        _playerListEntries = new Dictionary<PlayerRef, TextMeshProUGUI>();
    private Dictionary<PlayerRef, string> _playerNickNames = new Dictionary<PlayerRef, string>();
    private Dictionary<PlayerRef, int> _playerScores = new Dictionary<PlayerRef, int>();
    private Dictionary<PlayerRef, int> _playerLives = new Dictionary<PlayerRef, int>();
    [SerializeField] private TextMeshProUGUI _playerOverviewEntryPrefab = null;

    public void AddEntry(PlayerRef playerRef)
    {
        if (_playerListEntries.ContainsKey(playerRef)) return;

        var entry = Instantiate(_playerOverviewEntryPrefab, this.transform);
        entry.transform.localScale = Vector3.one;

        string nickName = String.Empty;
        int lives = 0;
        int score = 0;

        _playerNickNames.Add(playerRef, nickName);
        _playerScores.Add(playerRef, score);
        _playerLives.Add(playerRef, lives);

        _playerListEntries.Add(playerRef, entry);

        UpdateEntry(playerRef, entry);
    }

    public void RemoveEntry(PlayerRef playerRef)
    {
        if (_playerListEntries.TryGetValue(playerRef, out var entry) == false) return;

        if (entry != null)
        {
            Destroy(entry.gameObject);
        }

        _playerNickNames.Remove(playerRef);
        _playerScores.Remove(playerRef);
        _playerLives.Remove(playerRef);

        _playerListEntries.Remove(playerRef);
    }

    public void UpdateLives(PlayerRef player, int lives)
    {
        if (_playerListEntries.TryGetValue(player, out var entry) == false) return;

        _playerLives[player] = lives;
        UpdateEntry(player, entry);
    }

    public void UpdateScore(PlayerRef player, int score)
    {
        if (_playerListEntries.TryGetValue(player, out var entry) == false) return;

        _playerScores[player] = score;
        UpdateEntry(player, entry);
    }

    public void UpdateNickName(PlayerRef player, string nickName)
    {
        if (_playerListEntries.TryGetValue(player, out var entry) == false) return;

        _playerNickNames[player] = nickName;
        UpdateEntry(player, entry);
    }

    private void UpdateEntry(PlayerRef player, TextMeshProUGUI entry)
    {
        var nickName = _playerNickNames[player];
        var score = _playerScores[player];
        var lives = _playerLives[player];

        entry.text = $"{nickName}\nScore: {score}\nLives: {lives}";
    }
}
