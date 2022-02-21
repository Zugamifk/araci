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

    public void MeleeAttack(Attack attackPrefab, Vector3 direction, AttackInfo attack)
    {
        var a = Object.Instantiate(attackPrefab);
        a.Spawn(Services.Find<Character>().transform.position, direction, attack);
    }

    public void DoExplosion(Attack area, Vector3 position, AttackInfo attack)
    {
        var a = Object.Instantiate(area);
        a.Spawn(position, Vector3.up, attack);
    }

    public void DoAttack(IDamageable damageable, AttackInfo attack)
    {
        var pc = Services.Find<PlayerController>();
        var dmg = pc.CalculateDamage(attack.BaseDamage);
        damageable.DoDamage(dmg);

        if (damageable is Enemy e)
        {
            Services.Find<UI>().SpawnDamageCounter(dmg, e.transform.position);
        }
    }
}
