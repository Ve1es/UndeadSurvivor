using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : NetworkBehaviour
{
    //private List<NetworkObject> _spawnedEnnemies;
    public Transform spawnPoint;
    private bool _isSpawning = false;

    public void StartSpawnEnemy(float spawnInterval, List<NetworkPrefabRef> enemies)
    {
        _isSpawning = true;
        StartCoroutine(SpawnMobs(spawnInterval, enemies));
    }
    public void StopSpawnEnemy()
    {
        _isSpawning = false;
        //foreach(NetworkObject enemy in _spawnedEnnemies)
        //{
        //    Runner.Despawn(enemy);
        //}
        //_spawnedEnnemies.Clear();
    }

    IEnumerator SpawnMobs(float spawnInterval, List<NetworkPrefabRef> enemies)
    {
        while (_isSpawning)
        {
            if (Runner != null)
            {
                //_spawnedEnnemies.Add(Runner.Spawn(enemies[Random.Range(0, enemies.Count-1)], spawnPoint.position, Quaternion.identity));
                Runner.Spawn(enemies[Random.Range(0, enemies.Count - 1)], spawnPoint.position, Quaternion.identity);
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
