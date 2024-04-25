using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyState
{
    void Enter();
    void Exit();
    void Update(GameObject _player);
}

//public abstract class IEnemyState
//{
//    public abstract void Enter();
//    public abstract void Update();
//    public abstract void Exit();
//}