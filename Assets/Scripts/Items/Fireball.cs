using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : AttackItem
{
    [SerializeField]
    Bullet m_Bullet;
    [SerializeField]
    int m_BaseBulletDamage;
    [SerializeField]
    GameObject m_Explosion;
    [SerializeField]
    int m_BaseExplosionDamage;

    public override void Attack(ItemState state)
    {
        var ac = Services.Find<AttackController>();
        var target = ac.GetClosestTarget(this);
        if(target!=null)
        {
            var dir = (target.position - Services.Find<Character>().transform.position).normalized;
            Bullet.OnImpactCallback onExplode = state.Level < 3 ? null : OnExplode;
            var attack = new AttackInfo()
            {
                BaseDamage = m_BaseBulletDamage
            };
            ac.ShootBullet(m_Bullet, dir, attack, onExplode);
        }
    }

    void OnExplode(Bullet bullet)
    {
        Services.Find<AttackController>().DoExplosion(m_Explosion, bullet.transform.position);
    }
}
