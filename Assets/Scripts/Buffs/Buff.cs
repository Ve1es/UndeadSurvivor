using Fusion;
using UnityEngine;

public abstract class Buff : NetworkBehaviour
{
    public abstract void FindEffect(Collider2D other);
    public abstract void Destroy();
}
