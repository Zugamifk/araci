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
        foreach(var target in _attackTargets)
        {
            Debug.Log(target);
        }
    }
}
