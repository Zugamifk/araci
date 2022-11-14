using StateMachines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InputState : IState
{
    public InputStateContext Context { get; set; }

    public virtual void EnterState()
    {
    }

    public abstract IState UpdateState();
}
