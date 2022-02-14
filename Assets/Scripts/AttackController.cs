using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController
{
    public AttackController()
    {

    }


    public Transform GetClosestTarget(Item item)
    {
        AttackRadius radius = Services.Find<Character>().GetAttackRadius(item);
        return radius.GetNearestTarget();
    }

    public void ShootBullet(Bullet bullet, Vector3 direction, Bullet.OnImpactCallback onImpact)
    {
        var b = Object.Instantiate(bullet);
        b.gameObject.SetActive(true);
        b.transform.position = Services.Find<Character>().transform.position;
        b.Direction = direction;
        b.OnImpact += onImpact;
    }

    public void DoExplosion(GameObject fx, Vector3 position)
    {
        var e = Object.Instantiate(fx);
        e.gameObject.SetActive(true);
        e.transform.position = position;
    }
}
