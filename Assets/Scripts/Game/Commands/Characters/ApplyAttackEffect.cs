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
        foreach(var id in _attackTargets)
        {
            var target = model.Characters.GetItem(id);
            if (target == null) continue;
            target.Health.CurrentHealth -= attack.Damage;
            if(target.Health.CurrentHealth <=0)
            {
                new KillEnemy(id).Execute(model);
            }
        }
    }
}
