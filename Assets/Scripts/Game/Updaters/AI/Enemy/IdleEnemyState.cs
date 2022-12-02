using StateMachines;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Codice.Client.Common.WebApi.WebApiEndpoints;
using Random = UnityEngine.Random;

public class IdleEnemyState : EnemyBehaviourState
{
    float _lastJumpTime;
    public IdleEnemyState(Guid id) : base(id)
    {
    }

    public override void EnterState()
    {
        Game.Do(new StopCharacter(_id));
        _lastJumpTime = Game.Model.Time.Time + Random.value;
    }

    public override IState UpdateState()
    {
        if(Game.Model.Time.Time - _lastJumpTime > 1)
        {
            var player = Game.Model.Characters.GetItem(Game.Model.Player.Id);
            var agent = Game.Model.Characters.GetItem(_id);
            if(player == null || agent == null)
            {
                return this;
            }

            var dir = (player.Movement.Position - agent.Movement.Position).normalized;
            return new EnemyJumpState(_id, dir, 5, .4f);
        }
        return this;
    }
}
