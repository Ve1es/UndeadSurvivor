using UnityEngine;
using Fusion;
using TMPro;

public class StartMenu : MonoBehaviour
{
    private NetworkRunner _runnerInstance = null;

    [SerializeField] private NetworkRunner _networkRunnerPrefab = null;
    [SerializeField] private TMP_InputField _roomName = null;
    [SerializeField] private string _gameSceneName = null;
    
    public void StartHost()
    {
        StartGame(GameMode.AutoHostOrClient, _roomName.text, _gameSceneName);
    }

    public void StartClient()
    {
        StartGame(GameMode.Client, _roomName.text, _gameSceneName);
    }

    private async void StartGame(GameMode mode, string roomName, string sceneName)
    {
        _runnerInstance = FindObjectOfType<NetworkRunner>();
        if (_runnerInstance == null)
        {
            _runnerInstance = Instantiate(_networkRunnerPrefab);
        }

        _runnerInstance.ProvideInput = true;

        var startGameArgs = new StartGameArgs()
        {
            GameMode = mode,
            SessionName = roomName,
            ObjectProvider = _runnerInstance.GetComponent<NetworkObjectPoolDefault>(),
        };

        await _runnerInstance.StartGame(startGameArgs);

        if (_runnerInstance.IsServer)
        {
            _runnerInstance.LoadScene(sceneName);
        }
    }
}
