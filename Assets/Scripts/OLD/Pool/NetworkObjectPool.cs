using Fusion;
using System.Collections.Generic;
using UnityEngine;

public class NetworkObjectPool: NetworkBehaviour
{
    public GameObject prefab;
    public int poolSize = 10;

    private List<GameObject> objectsPool = new List<GameObject>();

    public override void Spawned()
    {
        for (int i = 0; i < poolSize; i++)
        {
            //GameObject obj = Runner.Spawn(prefab, new Vector3(0,0,0));
            //obj.SetActive(false);
            //objectsPool.Add(obj);
        }
    }

    public GameObject GetObjectFromPool()
    {
        foreach (var obj in objectsPool)
        {
            if (!obj.activeInHierarchy)
            {
                obj.SetActive(true);
                return obj;
            }
        }
        GameObject newObj = Instantiate(prefab);
        newObj.SetActive(true);
        objectsPool.Add(newObj);
        return newObj;
    }

    public void ReturnObjectToPool(GameObject obj)
    {
        obj.SetActive(false);
    }
}
