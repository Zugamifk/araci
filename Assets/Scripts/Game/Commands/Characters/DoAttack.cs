using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct DoAttack : ICommand
{
    Guid _attackerId;
    Vector2 _targetPosition;

    public DoAttack(Guid attackerId, Vector2 targetPosition)
    {
        _attackerId = attackerId;
        _targetPosition = targetPosition;
    }

    public void Execute(GameModel model)
    {
        var attacker = model.Characters.GetItem(_attackerId);
        var action = new ActionModel()
        {
            Key = Actions.ATTACK,
            TargetPosition = _targetPosition,
        };
        action.AnimationState.Key = Animation.ATTACK;
        attacker.CurrentAction.Value = action;

        var cooldownService = Services.Get<ICooldownService>();
        cooldownService.StartCooldown(attacker.Attack.Cooldown);
    }
}
