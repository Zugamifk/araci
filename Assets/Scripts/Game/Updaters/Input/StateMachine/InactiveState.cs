using StateMachines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InactiveState : InputState
{
    public override IState UpdateState()
    {
        // Do nothing;

        return this;
    }
}
