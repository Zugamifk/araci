using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Behaviour
{
    public class PiperBehaviour : AgentBehaviour<PiperBehaviourModel>
    {
        public PiperBehaviour(Guid id) : base(id)
        {
            currentState = new WanderState(id);
        }

        protected override BehaviourState TransitionState(AIModel behaviour, PiperBehaviourModel agent)
        {
            return null;
        }
    }
}