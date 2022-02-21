using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCracker : AttackItem
{
    [SerializeField]
    FireCrackerAttack m_ProjectilePrefab;
    [SerializeField]
    int m_BaseBulletDamage;
    [SerializeField]
    int m_BaseExplosionDamage;
    [SerializeField]
    float m_BaseExplosionRadius;
    [SerializeField]
    int m_BaseBurningAreaDamage;
    [SerializeField]
    float m_BaseBurningAreaRadius;
    [SerializeField]
    float m_BaseBurningAreaInterval;

    public override void Attack(ItemState state)
    {
        var ac = Services.Find<AttackController>();
        var target = ac.GetClosestTarget(this);
        if(target!=null)
        {
            var pos = Services.Find<Character>().transform.position;
            var dir = (target.position - pos).normalized;
            var p = Instantiate(m_ProjectilePrefab);
            p.Enable(pos, dir, 
                new AttackInfo() { BaseDamage = m_BaseBulletDamage, BaseSpeed = 5 }, 
                new AttackInfo() { BaseArea = m_BaseExplosionRadius, BaseDamage = m_BaseExplosionDamage },
                new AttackInfo() { BaseArea = m_BaseBurningAreaRadius, BaseDamage = m_BaseBurningAreaDamage, BaseInterval = m_BaseBurningAreaInterval });
        }
    }
}
