using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : NetworkBehaviour
{
    private bool _isSpawning = false;
    public Transform[] spawnPoints;

    public void StartSpawnEnemy(float spawnInterval, List<NetworkPrefabRef> enemies)
    {
        _isSpawning = true;
        StartCoroutine(SpawnMobs(spawnInterval, enemies));
    }
    public void StopSpawnEnemy()
    {
        _isSpawning = false;
    }

    IEnumerator SpawnMobs(float spawnInterval, List<NetworkPrefabRef> enemies)
    {
        while (_isSpawning)
        {
            if (Runner != null)
            {
                Runner.Spawn(enemies[Random.Range(0, enemies.Count - 1)], spawnPoints[Random.Range(0, spawnPoints.Length - 1)].position, Quaternion.identity);
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
