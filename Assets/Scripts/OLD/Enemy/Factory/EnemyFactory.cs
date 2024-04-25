using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    public enum EnemyType
    {
        RegularZombie,
        PumpedZombie,
        Skeleton
    }

    public static Enemy CreateEnemy(EnemyType type)
    {
        switch (type)
        {
            case EnemyType.RegularZombie:
                return new RegularZombie();
            case EnemyType.PumpedZombie:
                return new PumpedZombie();
            case EnemyType.Skeleton:
                return new Skeleton();
            default:
                throw new System.ArgumentException("Unsupported enemy type");
        }
    }
}
