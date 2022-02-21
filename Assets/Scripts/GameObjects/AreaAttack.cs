using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaAttack : Attack
{
    [SerializeField]
    AreaAttackTrigger m_Area;

    private void OnEnable()
    {
        m_Area.OnEnemyEnter += Damage;
        m_Area.GetComponent<Collider2D>().enabled = true;
    }

    void Damage(IDamageable enemy)
    {
        Services.Find<AttackController>().DoAttack(enemy, m_Attack);
    }

    public void End()
    {
        Destroy(gameObject);
    }

}
