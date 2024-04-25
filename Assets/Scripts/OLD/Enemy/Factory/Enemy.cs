using Fusion;

public abstract class Enemy : NetworkBehaviour
{
    public abstract void Activate();
    public abstract void MoveBehavior();
    public abstract void AttackBehavior();
    public abstract void Destroy();
}
