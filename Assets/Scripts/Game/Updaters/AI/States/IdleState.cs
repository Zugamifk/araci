using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Behaviour
{
    public class IdleState : BehaviourState
    {
        float _lastJumpTime;
        public IdleState(Guid id) : base(id)
        {
        }

        public override void InitializeState(GameModel model)
        {
            Game.Do(new StopCharacter(id));
            _lastJumpTime = Game.Model.Time.Time + Random.value;
        }

        public override void UpdateState(GameModel model)
        {
            if (Game.Model.Time.Time - _lastJumpTime > 1)
            {
                var player = Game.Model.Characters.GetItem(Game.Model.Player.Id);
                var agent = Game.Model.Characters.GetItem(id);
                if (player == null || agent == null)
                {
                    return;
                }

                var dir = (player.Movement.Position - agent.Movement.Position).normalized;
                //return new JumpState(id, dir, 5, .4f);
            }
        }
    }
}