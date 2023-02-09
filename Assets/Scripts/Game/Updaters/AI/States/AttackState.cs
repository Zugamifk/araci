using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Behaviour
{
    public class AttackState : TimedActionState<AttackStateModel>
    {
        Vector3 direction;
        float duration;

        public AttackState(Guid id, Vector3 direction, float duration)
            : base(id)
        {
            this.direction = direction;
            this.duration = duration;
        }

        protected override AttackStateModel InitializeState(AIModel behaviourModel)
        {
            Game.Do(new DoAttack(id, direction));

            var model = new AttackStateModel()
            {
                Direction = direction,
            };
            InitializeTimedAction(model, duration);

            return model;
        }

        protected override void UpdateState(AttackStateModel stateModel)
        {
            UpdateCanTransition(stateModel);
        }
    }
}