using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Behaviour
{
    public class JumpState : TimedActionState<JumpStateModel>
    {
        Vector3 direction;
        float speed;
        float duration;

        public JumpState(Guid id, Vector3 direction, float speed, float duration)
            : base(id)
        {
            this.direction = direction;
            this.speed = speed; 
            this.duration = duration;
        }

        protected override JumpStateModel InitializeState(AIModel behaviourModel)
        {
            Game.Do(new DoJump(id, direction, speed));

            var model = new JumpStateModel()
            {
                Direction = direction,
                Speed = speed
            };
            InitializeTimedAction(model, duration);

            return model;
        }

        protected override void UpdateState(JumpStateModel stateModel)
        {
            UpdateCanTransition(stateModel);
        }
    }
}