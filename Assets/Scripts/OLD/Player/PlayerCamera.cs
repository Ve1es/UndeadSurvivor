using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : NetworkBehaviour
{
    public override void Spawned()
    {
        if (HasInputAuthority)
        {
            GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
            camera.GetComponent<TopDownCamera>().target = gameObject.transform;
        }
    }
}
