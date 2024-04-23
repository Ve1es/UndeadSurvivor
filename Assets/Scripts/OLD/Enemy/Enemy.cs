using Fusion;
using UnityEngine;

public abstract class Enemy : NetworkBehaviour
{
    protected GameObject playerObject;
    public abstract void Activate();

    public abstract void Destroy();
}
