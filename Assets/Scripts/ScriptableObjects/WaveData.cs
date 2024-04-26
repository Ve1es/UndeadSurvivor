using Fusion;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWaveData", menuName = "Wave Data", order = 2)]
public class WaveData : ScriptableObject
{
    [SerializeField]
    private List<NetworkPrefabRef> _enemies;
    [SerializeField]
    private List<NetworkPrefabRef> _buffs;
    [SerializeField]
    private float _waveDuration;
    [SerializeField]
    private float _breakTime;
    [SerializeField]
    private float _enemySpawnTime;
    [SerializeField]
    private float _bonusesSpawnTime;

    public List<NetworkPrefabRef> Enemies
    {
        get
        {
            return _enemies;
        }
    }
    public List<NetworkPrefabRef> Buffs
    {
        get
        {
            return _buffs;
        }
    }
    public float WaveDuration
    {
        get
        {
            return _waveDuration;
        }
    }
    public float BreakTime
    {
        get
        {
            return _breakTime;
        }
    }
    public float EnemySpawnTime
    {
        get
        {
            return _enemySpawnTime;
        }
    }
    public float BonusesSpawnTime
    {
        get
        {
            return _bonusesSpawnTime;
        }
    }
}
