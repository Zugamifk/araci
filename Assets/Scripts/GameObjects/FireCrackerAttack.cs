using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCrackerAttack : MonoBehaviour
{
    [SerializeField]
    Attack m_ImpactAttack;
    [SerializeField]
    Attack m_ExplosionAttack;
    [SerializeField]
    FireCrackerBurningArea m_BurningAreaAttack;

    AttackInfo m_BurningAreaInfo;

    public void Enable(Vector3 pos, Vector3 direction, AttackInfo impactAttack, AttackInfo explosionAttack, AttackInfo burningAreaAttack)
    {
        m_ImpactAttack.Enable(pos, direction, impactAttack);
        GetComponent<Bullet>().OnImpact += OnImpact;

        m_ExplosionAttack.SetAttack(explosionAttack);

        m_BurningAreaInfo = burningAreaAttack;
    }

    void OnImpact(Bullet bullet, Enemy enemy)
    {
        m_ExplosionAttack.enabled = true;

        var pos = bullet.transform.position;
        var p = Instantiate(m_BurningAreaAttack);
        p.Enable(pos, m_BurningAreaInfo);
    }
}
