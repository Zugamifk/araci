using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct DoAttack : ICommand
{
    string _key;
    Guid _attackerId;
    Vector2 _targetPosition;

    public DoAttack(Guid attackerId, string key, Vector2 targetPosition)
    {
        _attackerId = attackerId;
        _key = key;
        _targetPosition = targetPosition;
    }

    public void Execute(GameModel model)
    {
        var attacker = model.Characters.GetItem(_attackerId);
        var action = attacker.CurrentAction;
        action.Key = _key;
        action.TargetPosition = _targetPosition;
        action.Cooldown.ReadyTime = model.TimeModel.Time + attacker.Attack.Cooldown;
    }
}
