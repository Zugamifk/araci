using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : AttackItem
{
    [SerializeField]
    Attack m_Attack;

    public override void Attack(ItemState state)
    {
        var ac = Services.Find<AttackController>();
        var target = ac.GetClosestTarget(this);
        if (target != null)
        {
            var dir = (target.position - Services.Find<Character>().transform.position).normalized;
            ac.MeleeAttack(m_Attack, dir, new AttackInfo() { BaseDamage = 20 });
        }
    }
}
