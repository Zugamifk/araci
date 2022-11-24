using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyAttackEffect : ICommand
{
    private Guid _id;
    private List<Guid> _attackTargets;

    public ApplyAttackEffect(Guid id, List<Guid> attackTargets)
    {
        _id = id;
        _attackTargets = attackTargets;
    }

    public void Execute(GameModel model)
    {
        var attack = model.Attacks.GetItem(_id);
        foreach(var target in _attackTargets)
        {
            var enemy = model.Characters.GetItem(target);
            if (enemy == null) continue;
            enemy.Health.CurrentHealth -= attack.Damage;
            if(enemy.Health.CurrentHealth <=0)
            {
                new KillEnemy(target).Execute(model);
            }
        }
    }
}
