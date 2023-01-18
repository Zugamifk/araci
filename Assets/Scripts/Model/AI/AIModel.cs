using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Behaviour
{
    public class AIModel : IAIModel
    {
        public Guid Id { get; set; }
        public AgentModel Agent { get; set; }
        public BehaviourStateModel State { get; set; }
    }
}