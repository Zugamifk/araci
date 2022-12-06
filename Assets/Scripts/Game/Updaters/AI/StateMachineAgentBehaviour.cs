using StateMachines;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public abstract class StateMachineAgentBehaviour : AgentBehaviour
{
    protected StateMachine _aiState;

    protected StateMachineAgentBehaviour(Guid id) : base(id)
    {

    }

    public override void Update(GameModel model)
    {
        _aiState.Update();
    }
}
