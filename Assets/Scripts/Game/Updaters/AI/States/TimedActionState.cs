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
    }
}