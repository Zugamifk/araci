using StateMachines;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDoingActionState : InputState
{
    float _endTime;

    public PlayerDoingActionState(float duration)
    {
        _endTime = Game.Model.Time.Time + duration;
    }

    public override IState UpdateState()
    {
        var done = Game.Model.Time.Time > _endTime;
        if(done)
        {
            return new PlayerControlState();
        } else
        {
            return this;
        }
    }
}
