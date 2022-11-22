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
        var attack = new AttackModel()
        {
            SourceId = _attackerId,
            Key = _key,
            TargetPosition = _targetPosition
        };
        model.Attacks.AddItem(attack);
    }
}
