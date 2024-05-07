using Fusion;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveController : NetworkBehaviour
{
    private const string PLAYER_TAG = "Player";
    private const string ENEMY_TAG = "Enemy";
    private const int FIRST_WAVE_NUMBER = 0;
    private const float DESPAWN_RADIUS = 100;
    private int _waveNumber;
    private float _waveDuration;
    private float _breakTime;
    private float _enemySpawnTime;
    private float _buffSpawnTime;
    private bool _isBreak;
    private bool _isWave;
    private List<NetworkPrefabRef> _waveEnemies;
    private List<NetworkPrefabRef> _waveBuffs;
    [Networked] private TickTimer _timer { get; set; }
    [SerializeField] private AllPlayerTimer _allPlayerTimer;
    [SerializeField] private TMP_Text _gameRoundTimer;
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private BuffSpawner _buffSpawner;
    [SerializeField] private List<WaveData> _wavePool;
    [SerializeField] private GameStateController _gameStateController;
    

    public void StartWaves()
    {
        _waveNumber = FIRST_WAVE_NUMBER;
        StartWave();
    }
    private void StartWave()
    {
        if (_waveNumber < _wavePool.Count)
        {
            _waveDuration = _wavePool[_waveNumber].WaveDuration;
            _breakTime = _wavePool[_waveNumber].BreakTime;
            _enemySpawnTime = _wavePool[_waveNumber].EnemySpawnTime;
            _buffSpawnTime = _wavePool[_waveNumber].BonusesSpawnTime;
            _waveEnemies = _wavePool[_waveNumber].Enemies;
            _waveBuffs = _wavePool[_waveNumber].Buffs;
            StartBreak();
        }
        else
        {
            EndGameBehaviour();
        }
    }
    private void StartBreak()
    {
        _isBreak = true;
        _enemySpawner.StopSpawnEnemy();
        _buffSpawner.StopSpawnBuff();
        EndWaveKillAllEnemy();
        _timer = TickTimer.CreateFromSeconds(Runner, _breakTime);
    }
    private void StartFight()
    {
        _isBreak = false;
        _isWave = true;
        _timer = TickTimer.CreateFromSeconds(Runner, _waveDuration);
        _enemySpawner.StartSpawnEnemy(_enemySpawnTime, _waveEnemies);
        _buffSpawner.StartSpawnBuff(_buffSpawnTime, _waveBuffs);
    }

    public override void FixedUpdateNetwork()
    {
        if (_isBreak)
        {
            RPC_ChangeTimer(ConvertTimeFormat(_timer));
        }
        if (_isWave)
        {
            RPC_ChangeTimer(ConvertTimeFormat(_timer));
        }
        if (_timer.Expired(Runner) && _isBreak)
        {
            StartFight();
        }
        if (_timer.Expired(Runner) && _isWave)
        {
            _isWave = false;
            _waveNumber++;
            StartWave();
        }
    }
    public void EndGameBehaviour()
    {
        _enemySpawner.StopSpawnEnemy();
        _buffSpawner.StopSpawnBuff();
        EndWaveKillAllEnemy();
        _gameStateController.GameHasEnded();
    }

    [Rpc]
    public void RPC_ChangeTimer(string time)
    {
        _gameRoundTimer.text = time;
    }
    private string ConvertTimeFormat(TickTimer time)
    {
        int minutes = Mathf.FloorToInt(Mathf.RoundToInt(time.RemainingTime(Runner) ?? 0) / 60);
        int seconds = Mathf.FloorToInt(Mathf.RoundToInt(time.RemainingTime(Runner) ?? 0) % 60);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void EndWaveKillAllEnemy()
    {       
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, DESPAWN_RADIUS);
        foreach (Collider2D enemyCollider in enemies)
        {
            if (enemyCollider.CompareTag(ENEMY_TAG))
            {
                enemyCollider.gameObject.GetComponent<Health>().ReduceHP(enemyCollider.gameObject.GetComponent<Health>().GetHP());
            }
        }
    }
}
