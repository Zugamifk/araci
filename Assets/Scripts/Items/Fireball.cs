using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : AttackItem
{
    [SerializeField]
    Bullet m_Bullet;
    [SerializeField]
    GameObject m_Explosion;

    public override void Attack()
    {
        var ac = Services.Find<AttackController>();
        var target = ac.GetClosestTarget(this);
        if(target!=null)
        {
            var dir = (target.position - Services.Find<Character>().transform.position).normalized;
            ac.ShootBullet(m_Bullet, dir, OnExplode);
        }
    }

    void OnExplode(Bullet bullet)
    {
        var e = Instantiate(m_Explosion);
        e.gameObject.SetActive(true);
        e.transform.position = bullet.transform.position;
    }
}
