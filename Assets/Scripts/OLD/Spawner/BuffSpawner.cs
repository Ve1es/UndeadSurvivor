using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffSpawner : NetworkBehaviour
{
    public GameObject[] buffPrefabs;
    public Transform spawnCenter;
    public float spawnWidth = 27f;
    public float spawnHeight = 19f;
    public float spawnFrequency = 5f;
    private const float ZCOORDINATE = 0;

    public override void Spawned()
    {
        StartCoroutine(SpawnBuff());
    }

    IEnumerator SpawnBuff()
    {
        while (true)
        {
            GameObject buffPrefab = buffPrefabs[Random.Range(0, buffPrefabs.Length)];
            Vector3 randomPosition = new Vector3(Random.Range(-spawnHeight, spawnHeight), Random.Range(-spawnWidth, spawnWidth), 0);
            Runner.Spawn(buffPrefab, randomPosition, Quaternion.identity);
            yield return new WaitForSeconds(spawnFrequency);
        }
    }
}
