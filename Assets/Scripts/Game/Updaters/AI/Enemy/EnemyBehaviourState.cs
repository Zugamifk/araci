using StateMachines;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBehaviourState : IState
{
    protected Guid id;
    public EnemyBehaviourState(Guid id)
    {
        this.id = id;
    }

    public virtual void EnterState()
    {
    }

    public abstract IState UpdateState();
}
