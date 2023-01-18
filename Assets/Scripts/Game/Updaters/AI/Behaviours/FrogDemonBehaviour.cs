using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Behaviour
{
    public class FrogDemonBehaviour : AgentBehaviour<FrogDemonBehaviourModel>
    {
        public FrogDemonBehaviour(Guid id) : base(id)
        {
            currentState = new IdleState(id);
        }

        protected override BehaviourState TransitionState(AIModel behaviour, FrogDemonBehaviourModel agent)
        {
            return null;
        }
    }
}