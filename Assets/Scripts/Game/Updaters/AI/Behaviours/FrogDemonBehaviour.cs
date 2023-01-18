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

        protected override BehaviourState TransitionState(AIModel behaviour, FrogDemonBehaviourModel agent)
        {
            var cooldownService = Services.Get<ICooldownService>();
            if(cooldownService.IsReady(agent.JumpCooldown))
            {
                cooldownService.StartCooldown(agent.JumpCooldown);
                return GetJumpState();
            }

            return new IdleState(id);
        }

        JumpState GetJumpState()
        {
            var player = Game.Model.PlayerCharacter;
            var agent = Game.Model.Characters.GetItem(id);
            var dir = (player.Movement.Position - agent.Movement.Position).normalized;
            return new JumpState(id, dir, 5, .4f);
        }
    }
}