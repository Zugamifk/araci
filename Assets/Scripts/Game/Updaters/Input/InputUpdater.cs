using StateMachines;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputUpdater : IUpdater
{
    StateMachine _inputStateMachine;
    InputStateContext _inputStateContext;

    public InputUpdater()
    {
        _inputStateContext = new();
        // put start state here
        _inputStateMachine = new StateMachine(new InactiveState() { Context = _inputStateContext });
    }

    public void Update(GameModel model)
    {
        _inputStateMachine.Update();
    }
}
