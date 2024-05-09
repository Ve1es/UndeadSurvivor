using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffSpawner : NetworkBehaviour
{
    private const float Z_Coordinate = 0;
    private const float Map_Width = 27f;
    private const float Map_Height = 19f;
    private const float SpawnFrequency = 5f;

    private bool _isSpawning = false;

    [SerializeField] private Transform _spawnCenter;
    [SerializeField] private float _spawnWidth = Map_Width;
    [SerializeField] private float _spawnHeight = Map_Height;
    [SerializeField] private float _spawnFrequency = SpawnFrequency;
   
    public void StartSpawnBuff(float _buffSpawnTime, List<NetworkPrefabRef> enemies)
    {
        _isSpawning = true;
        StartCoroutine(SpawnBuff(_buffSpawnTime, enemies));
    }
    public void StopSpawnBuff()
    {
        _isSpawning = false;
    }
    IEnumerator SpawnBuff(float _buffSpawnTime, List<NetworkPrefabRef> enemies)
    {
        while (_isSpawning)
        {
            NetworkPrefabRef buffPrefab = enemies[Random.Range(0, enemies.Count)];
            Vector3 randomPosition = new Vector3(Random.Range(-_spawnWidth, _spawnWidth), Random.Range(-_spawnHeight, _spawnHeight), Z_Coordinate);
            Runner.Spawn(buffPrefab, randomPosition, Quaternion.identity);
            yield return new WaitForSeconds(_buffSpawnTime);
        }
    }
}
