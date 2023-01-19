using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Behaviour
{
    public class FrogDemonBehaviourModel : AgentModel
    {
        public CooldownModel JumpCooldown { get; set; } = new();
        public CooldownModel AttackCooldown { get; set; } = new();
    }
}