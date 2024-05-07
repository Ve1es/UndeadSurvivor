using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffSpawner : NetworkBehaviour
{
    private const float ZCOORDINATE = 0;
    private bool _isSpawning = false;
    public Transform spawnCenter;
    public float spawnWidth = 27f;
    public float spawnHeight = 19f;
    public float spawnFrequency = 5f;
   
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
            Vector3 randomPosition = new Vector3(Random.Range(-spawnWidth, spawnWidth), Random.Range(-spawnHeight, spawnHeight), ZCOORDINATE);
            Runner.Spawn(buffPrefab, randomPosition, Quaternion.identity);
            yield return new WaitForSeconds(_buffSpawnTime);
        }
    }
}
