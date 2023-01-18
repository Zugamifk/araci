using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Behaviour
{
    public abstract class TimedActionState<TTimedActionStateModel> : BehaviourState<TTimedActionStateModel>
        where TTimedActionStateModel : TimedActionStateModel
    {
        protected TimedActionState(Guid id) : base(id)
        {
        }

        protected void InitializeTimedAction(TTimedActionStateModel model, float duration)
        {
            model.EndTime = Game.Model.Time.Time + duration;
        }

        protected void UpdateCanTransition(TTimedActionStateModel stateModel)
        {
            if(Game.Model.Time.Time >= stateModel.EndTime)
            {
                CanTransition = true;
            }
        }
    }
}