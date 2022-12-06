using StateMachines;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiperBehaviour : StateMachineAgentBehaviour
{
    public PiperBehaviour(Guid id) : base(id)
    {
        _aiState = new StateMachine(new WanderState(id));
    }
}
