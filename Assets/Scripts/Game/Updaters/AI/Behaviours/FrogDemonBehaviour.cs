using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Behaviour
{
    public class FrogDemonBehaviour : AgentBehaviour<FrogDemonModel>
    {
        public FrogDemonBehaviour(Guid id) : base(id)
        {
        }

        protected override void Update(BehaviourModel behaviour, FrogDemonModel agent)
        {
            throw new NotImplementedException();
        }
    }
}