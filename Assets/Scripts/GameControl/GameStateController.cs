using System;
using System.Collections.Generic;
using Fusion;
using UnityEngine;
public class GameStateController : NetworkBehaviour
{
    enum GameState
    {
        Starting,
        WaitingPlayerConnection,
        Running,
        Ending,
        ResultShow
    }
    [Networked] private GameState _gameState { get; set; }
    private List<NetworkBehaviourId> _playerDataNetworkedIds = new List<NetworkBehaviourId>();
    private LocalInputPoller _localInputPoller;

    [SerializeField] private WaveController _waveController;
    [SerializeField] private CharacterSpawner _characterSpawner;
    [SerializeField] private PlayerPool _playerPool;
    [SerializeField] private KillsList _killsList;
    [SerializeField] private PlayerDamageList _playerDamageList;

    [SerializeField] private EndGameStatistics _endGameResult;
    [SerializeField] private StartLevelUIChange _startLevelUIChange;
    [SerializeField] private JoystickMove _joysticMove;
    [SerializeField] private JoystickWeapon _joysticWeapon;


    private void Start()
    {
        _localInputPoller = FindObjectOfType<LocalInputPoller>();
        _localInputPoller.ConnectInputSystem(_joysticMove, _joysticWeapon);
    }

    public override void Spawned()
    {
        Runner.SetIsSimulated(Object, true);

        if (Object.HasStateAuthority == false) return;

        _gameState = GameState.Starting;

        _playerPool.ClearPool();
        _killsList.ClearPool();
        _playerDamageList.ClearPool();
    }

    public override void FixedUpdateNetwork()
    {
        switch (_gameState)
        {
            case GameState.Starting:
                UpdateStarting();
                break;
            case GameState.WaitingPlayerConnection:
                UpdateWaiting();
                break;
            case GameState.Running:
                UpdateRunning();
                break;
            case GameState.Ending:
                UpdateEnding();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void UpdateStarting()
    {
        if (Object.HasStateAuthority == false) return;

        _gameState = GameState.WaitingPlayerConnection;
    }

    private void UpdateWaiting()
    {
        if (_characterSpawner.SelectedCharacters != null)
        {
            if (_characterSpawner.SelectedCharacters.Count >= 1)
            {
                _characterSpawner.StartCharacterSpawner(this);

                RPC_ChangeInterface();
                _gameState = GameState.Running;
                _waveController.StartWaves();
            }
        }
    }
    private void UpdateRunning()
    {
        if(_playerPool.Players.Count <= 0) 
        {
            GameHasEnded();
        }
    }

    private void UpdateEnding()
    {
    }

    [Rpc]
    public void RPC_ChangeInterface()
    {
        _startLevelUIChange.Changeinterface();
    }
    public void GameHasEnded()
    {
        _endGameResult.gameObject.SetActive(true);
        _endGameResult.DisplayStatistic();
        _gameState = GameState.Ending;

    }
    public void TrackNewPlayer(NetworkBehaviourId playerDataNetworkedId)
    {
        _playerDataNetworkedIds.Add(playerDataNetworkedId);
    }
}
