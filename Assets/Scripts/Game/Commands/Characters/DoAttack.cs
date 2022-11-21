using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct DoAttack : ICommand
{
    string _key;
    Guid _attackerId;
    public DoAttack(Guid attackerId, string key)
    {
        _attackerId = attackerId;
        _key = key;
    }

    public void Execute(GameModel model)
    {
        var attack = new AttackModel()
        {
            SourceId = _attackerId,
            Key = _key
        };
        model.Attacks.AddItem(attack);
    }
}
