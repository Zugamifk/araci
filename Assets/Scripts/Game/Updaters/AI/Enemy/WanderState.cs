using StateMachines;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderState : EnemyBehaviourState
{
    const float REACH_THRESHOLD = .25f;
    Vector2 _destination;
    public WanderState(Guid id) : base(id)
    {

    }

    public override IState UpdateState()
    {
        var character = Game.Model.Characters.GetItem(_id);
        var to = _destination - character.Movement.Position;
        if (to.magnitude < REACH_THRESHOLD)
        {
            GetNewDestination();
        } else
        {
            Game.Do(new MoveCharacter(_id, to.normalized, Space.World));
        }
        return this;
    }

    void GetNewDestination()
    {

    }
}
