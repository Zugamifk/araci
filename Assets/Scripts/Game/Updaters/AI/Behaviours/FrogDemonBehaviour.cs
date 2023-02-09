using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Behaviour
{
    public class FrogDemonBehaviour : AgentBehaviour<FrogDemonAgentModel>
    {
        public FrogDemonBehaviour(Guid id) : base(id)
        {
            currentState = new IdleState(id);
            currentState.CanTransition = true;
        }

        protected override BehaviourState TransitionState(AIModel ai, FrogDemonAgentModel behaviour)
        {
            var cooldownService = Services.Get<ICooldownService>();
            var player = Game.Model.PlayerCharacter;
            if(player == null)
            {
                return new IdleState(id);
            }

            var agent = Game.Model.Characters.GetItem(id);
            var toAgent = player.Movement.Position - agent.Movement.Position;
            if (toAgent.magnitude < agent.Attack.Range)
            {
                if (cooldownService.IsReady(behaviour.AttackCooldown))
                {
                    cooldownService.StartCooldown(behaviour.AttackCooldown);
                    return GetAttackState(toAgent.normalized);
                }
            }
            else if (cooldownService.IsReady(behaviour.JumpCooldown))
            {
                cooldownService.StartCooldown(behaviour.JumpCooldown);
                return GetJumpState(toAgent.normalized);
            }

            return new IdleState(id);
        }

        AttackState GetAttackState(Vector2 direction)
        {
            return new AttackState(id, direction, .4f);
        }

        JumpState GetJumpState(Vector2 direction)
        {
            return new JumpState(id, direction, 5, .4f);
        }
    }
}