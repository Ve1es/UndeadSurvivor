using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Collections.Unicode;

public class DeathState : IEnemyState
{
    private GameObject _enemy;
    public DeathState(GameObject enemy)
    {
        _enemy = enemy;
    }
    public void Enter()
    {

    }

    public void Exit()
    {
        
    }
    public void Update(GameObject _player)
    {

    }
}
