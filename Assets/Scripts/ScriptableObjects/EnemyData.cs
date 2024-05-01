using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyData", menuName = "Enemy Data", order = 3)]
public class EnemyData : ScriptableObject
{
    [SerializeField]
    private string _enemyName;
    [SerializeField]
    private float _timeBetweenAttacks;
    [SerializeField]
    private float _movingSpeed;
    [SerializeField]
    private float _damage;
    [SerializeField]
    private float _attackDistance;
    [SerializeField]
    private float _hp;


    public string EnemyName
    {
        get
        {
            return _enemyName;
        }
    }
    public float TimeBetweenAttacks
    {
        get
        {
            return _timeBetweenAttacks;
        }
    }
    public float MovingSpeed
    {
        get
        {
            return _movingSpeed;
        }
    }
    public float AttackDistance
    {
        get
        {
            return _attackDistance;
        }
    }
    public float Damage
    {
        get
        {
            return _damage;
        }
    }
    public float HP
    {
        get
        {
            return _hp;
        }
    }
}
