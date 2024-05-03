using Fusion;
using UnityEngine;
using System.Collections;

public class DespawnByTimer : NetworkBehaviour
{
    [SerializeField] private float _despawnTime = 5;
    public override void Spawned()
    {
        StartCoroutine(DespawnTimer());
    }
    IEnumerator DespawnTimer()
    {
        yield return new WaitForSeconds(_despawnTime);
        Runner.Despawn(Object);
    }

}
