using Fusion;
using UnityEngine;

public class SpawnDeathPrefab : NetworkBehaviour
{
    [SerializeField]
    private NetworkPrefabRef _afterDeathObject;  

    public void SpawnDeathObject()
    {
        Runner.Spawn(_afterDeathObject, gameObject.transform.position, Quaternion.identity);
    }
}
