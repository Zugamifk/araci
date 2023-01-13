using StateMachines;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyBehaviourState
{
    Vector3 direction;
    float startTime;
    float duration;

    public EnemyAttackState(Guid id, Vector3 direction, float duration)
        : base(id)
    {
        this.direction = direction;
        startTime = Game.Model.Time.Time;
        this.duration = duration;
    }

    public override void EnterState()
    {
        Game.Do(new DoAttack(id, Vector3.zero));
    }

    public override IState UpdateState()
    {
        if (Game.Model.Time.Time - startTime > duration)
        {
            return new IdleEnemyState(id);
        }
        else return this;
    }
}
