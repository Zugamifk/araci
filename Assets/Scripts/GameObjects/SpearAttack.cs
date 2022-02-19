using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearAttack : Attack
{
    [SerializeField]
    AreaAttack m_Area;

    private void Awake()
    {
        m_Area.OnEnemyEnter += Damage;
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
