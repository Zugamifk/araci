using StateMachines;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachineAgentBehaviour : AgentBehaviour
{
    protected StateMachine _aiState;

    protected StateMachineAgentBehaviour(Guid id) : base(id)
    {
    }
}
