using StateMachines;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDoingActionState : InputState
{
    float _endTime;
    Action _onEnd;
    public PlayerDoingActionState(float duration, Action onEnd)
    {
        _endTime = Game.Model.Time.Time + duration;
        _onEnd = onEnd;
    }

    public override IState UpdateState()
    {
        var done = Game.Model.Time.Time > _endTime;
        if(done)
        {
            _onEnd?.Invoke();
            return new PlayerControlState();
        } else
        {
            return this;
        }
    }
}
