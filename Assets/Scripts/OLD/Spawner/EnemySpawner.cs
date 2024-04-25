using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : NetworkBehaviour
{
    public GameObject mobPrefab;
    public Transform spawnPoint;
    public float spawnInterval = 1f;

    void Start()
    {
        StartCoroutine(SpawnMobs());
    }

    IEnumerator SpawnMobs()
    {
        while (true)
        {
            if (Runner != null)
            { Runner.Spawn(mobPrefab, spawnPoint.position, Quaternion.identity); }

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
