using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    public IEnemyState CurrentState { get; set; }

    public void Initialize(IEnemyState startState)
    {
        CurrentState = startState;
        CurrentState.Enter();
    }
    public void ChangeState(IEnemyState newState)
    {
        CurrentState.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }
}
