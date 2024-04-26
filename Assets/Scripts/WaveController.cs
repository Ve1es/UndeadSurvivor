using Fusion;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveController : NetworkBehaviour
{
    private int _waveNumber;
    [SerializeField] private List<WaveData> _wavePool;
    [Networked] private TickTimer _timer { get; set; }
    //[Networked] private TickTimer _timerBreak { get; set; }
    //[Networked] private TickTimer _timerWave { get; set; }

    [SerializeField] private TMP_Text _gameRoundTimer;
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private BuffSpawner _buffSpawner;
    private float _waveDuration=0;
    private float _breakTime=0;
    private float _enemySpawnTime=0;
    private float _buffSpawnTime=0;
    private bool _isBreak;
    private bool _isWave;
    private List<NetworkPrefabRef> _waveEnemies;
    private List<NetworkPrefabRef> _waveBuffs;

    public void StartWaves()
    {
        _waveNumber = 0;
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
            ///EndGame
        }
    }


    private void StartBreak()
    {
        _isBreak = true;
        _enemySpawner.StopSpawnEnemy();
        _buffSpawner.StopSpawnBuff();
        //_timerBreak = TickTimer.CreateFromSeconds(Runner, _breakTime);
        _timer = TickTimer.CreateFromSeconds(Runner, _breakTime);
    }
    private void StartFight()
    {
        _isBreak = false;
        _isWave = true;
        _timer = TickTimer.CreateFromSeconds(Runner, _waveDuration);
        //_timerWave = TickTimer.CreateFromSeconds(Runner, _waveDuration);
        _enemySpawner.StartSpawnEnemy(_enemySpawnTime, _waveEnemies);
        _buffSpawner.StartSpawnBuff(_buffSpawnTime, _waveBuffs);
    }

    public override void FixedUpdateNetwork()
    {
        if(_isBreak)
        {
            _gameRoundTimer.text =
              _gameRoundTimer.text = ConvertTimeFormat(_timer);
        }
        if(_isWave)
        {
            _gameRoundTimer.text = ConvertTimeFormat(_timer);
        }
        if(_timer.Expired(Runner)&&_isBreak)
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

    private string ConvertTimeFormat(TickTimer time)
    {
        int minutes = Mathf.FloorToInt(Mathf.RoundToInt(time.RemainingTime(Runner) ?? 0) / 60);
        int seconds = Mathf.FloorToInt(Mathf.RoundToInt(time.RemainingTime(Runner) ?? 0) % 60);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
