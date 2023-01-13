using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Behaviour
{
    //public class JumpState : TimedActionState<JumpStateModel>
    //{
    //    Vector3 direction;
    //    float speed;
    //    float duration;

    //    public JumpState(Guid id, Vector3 direction, float jumpSpeed, float jumpDuration)
    //        : base(id)
    //    {
    //        this.direction = direction;
    //        speed = jumpSpeed;
    //        duration = jumpDuration;
    //    }

    //    protected override JumpStateModel InitializeState(BehaviourModel behaviourModel)
    //    {
    //        Game.Do(new DoDash(id, direction, speed));
    //        return new JumpStateModel()
    //        {
    //            StartTime = Game.Model.Time.Time,
    //            Duration = duration,
    //            Direction = direction,
    //            Speed = speed
    //        };
    //    }

    //    protected override IState UpdateState(BehaviourModel behaviourModel, JumpStateModel stateModel)
    //    {
    //        if (Game.Model.Time.Time - stateModel.StartTime > stateModel.Duration)
    //        {
    //            return new IdleState(id);
    //        }
    //        else return this;
    //    }
    //}
}