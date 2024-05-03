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




    [SerializeField] private PlayerPool _playerPool;
    [Networked] private GameState _gameState { get; set; }

    [SerializeField] private WaveController _waveController;
    [SerializeField] private KillsList _killsList;
    [SerializeField] private PlayerDamageList _playerDamageList;
    [SerializeField] private GameObject _endGameResult;
    [SerializeField] private GameObject _startSoloGame;
    [SerializeField] private CharacterSpawner _characterSpawner;

    [SerializeField] private GameObject _kills;
    [SerializeField] private GameObject _hp;
    [SerializeField] private GameObject _ammo;
    [SerializeField] private GameObject _timer;
    [SerializeField] private GameObject _joysticLeft;
    [SerializeField] private GameObject _joysticRight;
    [SerializeField] private GameObject _joysticLeftButton;
    [SerializeField] private GameObject _joysticRightButton;

    private List<NetworkBehaviourId> _playerDataNetworkedIds = new List<NetworkBehaviourId>();
    private LocalInputPoller _localInputPoller;
    public JoystickMove joysticMove;
    public JoystickWeapon joysticWeapon;


    private void Start()
    {
        _localInputPoller = FindObjectOfType<LocalInputPoller>();
        _localInputPoller.ConnectInputSystem(joysticMove, joysticWeapon);
    }

    public override void Spawned()
    {
        // If the game has already started, find all currently active players' PlayerDataNetworked component Ids
        if (_gameState != GameState.Starting)
        {
            foreach (var player in Runner.ActivePlayers)
            {
                if (Runner.TryGetPlayerObject(player, out var playerObject) == false) continue;
                TrackNewPlayer(playerObject.GetComponent<PlayerDataNetworked>().Id);
            }
        } 

        // Set is Simulated so that FixedUpdateNetwork runs on every client instead of just the Host
        Runner.SetIsSimulated(Object, true);

        // --- This section is for all networked information that has to be initialized by the HOST
        if (Object.HasStateAuthority == false) return;

        _gameState = GameState.Starting;

        _playerPool.ClearPool();
        _killsList.ClearPool();
        _playerDamageList.ClearPool();
    }

    public override void FixedUpdateNetwork()
    {
        // Update the game display with the information relevant to the current game state
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
        // --- Host & Client
        // Display the remaining time until the game starts in seconds (rounded down to the closest full second)



        // --- Host
        if (Object.HasStateAuthority == false) return;
        //if (_timer.ExpiredOrNotRunning(Runner) == false) return;

        // Starts the Spaceship and Asteroids spawners once the game start delay has expired
        //FindObjectOfType<CharacterSpawner>().StartCharacterSpawner(this);

        // Switches to the Running GameState and sets the time to the length of a game session
        _gameState = GameState.WaitingPlayerConnection;
    }

    private void UpdateWaiting()
    {
        if (_characterSpawner._selectedCharacters != null)
        {
            if (_characterSpawner._selectedCharacters.Count >= 2)
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
        //if (_startSoloGame.activeSelf)
        //    RPC_ChangeInterface();
        if(_playerPool.players.Count <= 0) 
        {
            GameHasEnded();
        }
    }

    private void UpdateEnding()
    {


        //Runner.Shutdown();
    }

    public void StartSoloGame()
    {
        FindObjectOfType<CharacterSpawner>().StartCharacterSpawner(this);
        RPC_ChangeInterface();
        _gameState = GameState.Running;
        _waveController.StartWaves();
        
    }
    [Rpc]
    public void RPC_ChangeInterface()
    {
        _startSoloGame.SetActive(false);
        _kills.SetActive(true);
        _hp.SetActive(true);
        _ammo.SetActive(true);
        _timer.SetActive(true);
        _joysticLeft.SetActive(true);
        _joysticRight.SetActive(true);
        _joysticLeftButton.SetActive(true);
        _joysticRightButton.SetActive(true);
    }
    public void GameHasEnded()
    {
        _endGameResult.GetComponent<EndGameStatistics>().DisplayStatistic();
        _gameState = GameState.Ending;

    }
    public void TrackNewPlayer(NetworkBehaviourId playerDataNetworkedId)
    {
        _playerDataNetworkedIds.Add(playerDataNetworkedId);
    }
}
