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

        // Initialize the game state on the host
        _gameState = GameState.Starting;
        //_timer = TickTimer.CreateFromSeconds(Runner, _startDelay);
        
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
        FindObjectOfType<CharacterSpawner>().StartCharacterSpawner(this);
        


         // Switches to the Running GameState and sets the time to the length of a game session
         _gameState = GameState.WaitingPlayerConnection;
        
        //_timer = TickTimer.CreateFromSeconds(Runner, 100);

        //_timer = TickTimer.CreateFromSeconds(Runner, _gameSessionLength);
    }

    private void UpdateWaiting()
    {
        if (_playerPool.players.Count >= 2)
        {
            
            _gameState = GameState.Running;
            _waveController.StartWaves();
        }
    }
    private void UpdateRunning()
    {
        if(_startSoloGame.activeSelf)
        RPC_CloseSoloGameButton();
        // --- Host & Client
        // Display the remaining time until the game ends in seconds (rounded down to the closest full second)
        //_startEndDisplay.gameObject.SetActive(false);
        //_ingameTimerDisplay.gameObject.SetActive(true);
        //_ingameTimerDisplay.text =
        //   $"{Mathf.RoundToInt(_timer.RemainingTime(Runner) ?? 0).ToString("000")} seconds left";
        // _gameRoundTimer.text = $"Game Starts In {Mathf.RoundToInt(_timer.RemainingTime(Runner) ?? 0)}";

        // FindObjectOfType<BuffSpawner>().StartSpawnBuff();
        //FindObjectOfType<EnemySpawner>().SpawnEnemyWave();
    }

    private void UpdateEnding()
    {
        // --- Host & Client
        // Display the results and
        // the remaining time until the current game session is shutdown


       // _startEndDisplay.gameObject.SetActive(true);
       // _ingameTimerDisplay.gameObject.SetActive(false);
       // _startEndDisplay.text =
         //   $"{playerData.NickName} won with {playerData.Score} points. Disconnecting in {Mathf.RoundToInt(_timer.RemainingTime(Runner) ?? 0)}";

        // --- Host
        // Shutdowns the current game session.
        // The disconnection behaviour is found in the OnServerDisconnect.cs script

        //Runner.Shutdown();
    }

    public void StartSoloGame()
    {
        RPC_CloseSoloGameButton();
         _gameState = GameState.Running;
        _waveController.StartWaves();
    }
    [Rpc]
    public void RPC_CloseSoloGameButton() 
    {
        _startSoloGame.SetActive(false);
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
