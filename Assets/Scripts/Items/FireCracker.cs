using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCracker : AttackItem
{
    [SerializeField]
    Bullet m_Bullet;
    [SerializeField]
    int m_BaseBulletDamage;
    [SerializeField]
    int m_BaseExplosionDamage;
    [SerializeField]
    float m_BaseExplosionRadius;

    public override void Attack(ItemState state)
    {
        var ac = Services.Find<AttackController>();
        var target = ac.GetClosestTarget(this);
        if(target!=null)
        {
            var dir = (target.position - Services.Find<Character>().transform.position).normalized;
            Bullet.OnImpactCallback onExplode = OnExplode;
            var attack = new AttackInfo()
            {
                BaseDamage = m_BaseBulletDamage,
            };
            ac.ShootBullet(m_Bullet, dir, attack, onExplode);
        }
    }

    void OnExplode(Bullet bullet)
    {
        var attack = new AttackInfo()
        {
            BaseDamage = m_BaseExplosionDamage,
            BaseArea = m_BaseExplosionRadius
        };
        var explosion = bullet.GetComponentInChildren<AreaAttack>();
        explosion.Spawn(bullet.transform.position, Vector3.zero, attack);
        explosion.enabled = true;
    }
}
