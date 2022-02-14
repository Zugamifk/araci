using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackItem : Item
{
    public float BaseInterval;
    public abstract void Attack(ItemState state);

    public override ItemState GetNewState()
    {
        var state = base.GetNewState();
        state.RemainingInterval = BaseInterval;
        return state;
    }
}
