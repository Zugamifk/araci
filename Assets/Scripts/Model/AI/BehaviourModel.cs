using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Behaviour
{
    public class BehaviourModel : IBehaviourModel
    {
        public Guid Id { get; }
        public AgentModel Agent { get; set; }
        public BehaviourStateModel State { get; set; }
    }
}