using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayerStats", menuName = "Player Stats", order = 3)]

public class PlayerStats : ScriptableObject
{
    [SerializeField]
    private float _movingSpeed;
    [SerializeField]
    private float _hp;

    public float MovingSpeed
    {
        get
        {
            return _movingSpeed;
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
