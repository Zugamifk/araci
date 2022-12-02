using StateMachines;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBehaviourState : IState
{
    protected Guid _id;
    public EnemyBehaviourState(Guid id)
    {
        _id = id;
    }

    public virtual void EnterState()
    {
    }

    public abstract IState UpdateState();
}
