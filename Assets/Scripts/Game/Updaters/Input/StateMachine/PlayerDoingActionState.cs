using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Input
{
    public class PlayerDoingActionState : InputState
    {
        float _endTime;

        public PlayerDoingActionState(float duration)
        {
            _endTime = Game.Model.Time.Time + duration;
        }

        public override void Update(IInputStateMachine inputStateMachine)
        {
            var done = Game.Model.Time.Time > _endTime;
            if (done)
            {
                inputStateMachine.PopState();
            }
        }
    }
}