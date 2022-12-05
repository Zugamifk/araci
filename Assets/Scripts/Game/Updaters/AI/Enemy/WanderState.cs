using StateMachines;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderState : EnemyBehaviourState
{
    public WanderState(Guid id) : base(id)
    {

    }

    public override IState UpdateState()
    {
        return this;
    }
}
