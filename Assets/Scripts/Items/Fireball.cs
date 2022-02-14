using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : AttackItem
{
    [SerializeField]
    Bullet m_Bullet;
    [SerializeField]
    GameObject m_Explosion;

    public override void Attack(ItemState state)
    {
        var ac = Services.Find<AttackController>();
        var target = ac.GetClosestTarget(this);
        if(target!=null)
        {
            var dir = (target.position - Services.Find<Character>().transform.position).normalized;
            Bullet.OnImpactCallback onExplode = state.Level < 3 ? null : OnExplode;
            ac.ShootBullet(m_Bullet, dir, onExplode);
        }
    }

    void OnExplode(Bullet bullet)
    {
        
    }
}
