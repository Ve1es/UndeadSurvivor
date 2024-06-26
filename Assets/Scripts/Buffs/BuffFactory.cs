using UnityEngine;

public class BuffFactory : MonoBehaviour
{
    public enum EnemyType
    {
        MedChest,
        Ammo,
        Bomb
    }

    public static Buff CreateEnemy(EnemyType type)
    {
        switch (type)
        {
            case EnemyType.MedChest:
                return new MedChest();
            case EnemyType.Ammo:
                return new Ammo();
            case EnemyType.Bomb:
                return new Bomb();
            default:
                throw new System.ArgumentException("Unsupported enemy type");
        }
    }
}
