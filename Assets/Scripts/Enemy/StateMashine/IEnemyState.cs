using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyState
{
    void Enter();
    void Exit();
    void UpdateSM(CharacterPlayerController _player);
}