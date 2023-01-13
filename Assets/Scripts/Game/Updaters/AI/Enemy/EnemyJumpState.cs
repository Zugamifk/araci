using StateMachines;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyJumpState : EnemyBehaviourState
{
    Vector3 _direction;
    float _jumpStartTime;
    float _jumpSpeed;
    float _jumpDuration;

    public EnemyJumpState(Guid id, Vector3 direction, float jumpSpeed, float jumpDuration) 
        : base(id)
    {
        _direction = direction;
        _jumpStartTime = Game.Model.Time.Time;
        _jumpSpeed = jumpSpeed;
        _jumpDuration = jumpDuration;
    }

    public override void EnterState()
    {
        Game.Do(new DoDash(id, _direction, _jumpSpeed));
    }

    public override IState UpdateState()
    {
        if (Game.Model.Time.Time - _jumpStartTime > _jumpDuration)
        {
            return new IdleEnemyState(id);
        }
        else return this;
    }
}
