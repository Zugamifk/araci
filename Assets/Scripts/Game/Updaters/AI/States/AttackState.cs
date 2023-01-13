using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Behaviour
{
    //public class AttackState : BehaviourState<AttackStateModel>
    //{
    //    public AttackState(Guid id, Vector3 direction, float duration)
    //        : base(id)
    //    {
    //        this.direction = direction;
    //        startTime = Game.Model.Time.Time;
    //        this.duration = duration;
    //    }

    //    public override void EnterState()
    //    {
    //        Game.Do(new DoAttack(id, Vector3.zero));
    //    }

    //    public override IState ProcessState(GameModel model)
    //    {
    //        if (Game.Model.Time.Time - startTime > duration)
    //        {
    //            return new IdleState(id);
    //        }
    //        else return this;
    //    }
    //}
}