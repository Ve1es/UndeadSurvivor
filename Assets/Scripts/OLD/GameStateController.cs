using System;
using System.Collections.Generic;
using Fusion;
using TMPro;
using UnityEngine;
public class GameStateController : NetworkBehaviour
{
    enum GameState
    {
        Starting,
        Running,
        Ending
    }
    ///Displays///
    [SerializeField] private GameObject _kills;
    [SerializeField] private GameObject _hp;
    [SerializeField] private GameObject _ammo;
    [SerializeField] private GameObject _joystickRight;
    [SerializeField] private GameObject _joystickLeft;
    [SerializeField] private GameObject _loading;

    [SerializeField] private TMP_Text _gameRoundTimer;
    [SerializeField] private PlayerPool _playerPool;
    


    [Networked] private TickTimer _timer { get; set; }
    [Networked] private GameState _gameState { get; set; }

    [SerializeField] private WaveController _waveController;
    [SerializeField] private KillsList _killsList;
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
        // --- This section is for all information which has to be locally initialized based on the networked game state
        // --- when a CLIENT joins a game

       // _startEndDisplay.gameObject.SetActive(true);
       // _ingameTimerDisplay.gameObject.SetActive(false);

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
    }

    public override void FixedUpdateNetwork()
    {
        // Update the game display with the information relevant to the current game state
        switch (_gameState)
        {
            case GameState.Starting:
                UpdateStartingDisplay();
                break;
            case GameState.Running:
                UpdateRunningDisplay();
                break;
            case GameState.Ending:
                UpdateEndingDisplay();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }



    private void UpdateStartingDisplay()
    {
        // --- Host & Client
        // Display the remaining time until the game starts in seconds (rounded down to the closest full second)

       

        // --- Host
        if (Object.HasStateAuthority == false) return;
        if (_timer.ExpiredOrNotRunning(Runner) == false) return;

        // Starts the Spaceship and Asteroids spawners once the game start delay has expired
        FindObjectOfType<CharacterSpawner>().StartCharacterSpawner(this);
        _killsList.playersKills.Clear();


         // Switches to the Running GameState and sets the time to the length of a game session
         _gameState = GameState.Running;
        _waveController.StartWaves();
        _timer = TickTimer.CreateFromSeconds(Runner, 100);

        //_timer = TickTimer.CreateFromSeconds(Runner, _gameSessionLength);
    }

    private void UpdateRunningDisplay()
    {
        
        int minutes = Mathf.FloorToInt(Mathf.RoundToInt(_timer.RemainingTime(Runner) ?? 0) / 60);
        int seconds = Mathf.FloorToInt(Mathf.RoundToInt(_timer.RemainingTime(Runner) ?? 0) % 60);
        string a = string.Format("{0:00}:{1:00}", minutes, seconds);
        _gameRoundTimer.text = a;
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

    private void UpdateEndingDisplay()
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
        if (_timer.ExpiredOrNotRunning(Runner) == false) return;

        Runner.Shutdown();
    }
    private void GameHasEnded()
    {
        //throw new NotImplementedException();
    }
    public void TrackNewPlayer(NetworkBehaviourId playerDataNetworkedId)
    {
        _playerDataNetworkedIds.Add(playerDataNetworkedId);
    }
    
}
