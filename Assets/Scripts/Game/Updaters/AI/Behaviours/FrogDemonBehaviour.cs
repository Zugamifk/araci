using Codice.Client.BaseCommands.Merge;
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
            currentState.CanTransition = true;
        }

        protected override BehaviourState TransitionState(AIModel ai, FrogDemonBehaviourModel behaviour)
        {
            var cooldownService = Services.Get<ICooldownService>();
            var player = Game.Model.PlayerCharacter;
            var agent = Game.Model.Characters.GetItem(id);
            var toAgent = player.Movement.Position - agent.Movement.Position;
            if (toAgent.magnitude < agent.Attack.Range && cooldownService.IsReady(behaviour.AttackCooldown))
            {
                cooldownService.StartCooldown(behaviour.AttackCooldown);
                return GetAttackState(toAgent.normalized);
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