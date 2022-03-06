using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCrackerAttack : MonoBehaviour
{
    [SerializeField]
    Attack m_ImpactAttack;
    [SerializeField]
    FireCrackerBurningArea m_BurningAreaAttack;
    [SerializeField]
    GameObject m_ExplosionEffect;

    AttackInfo m_ExplosionInfo;
    AttackInfo m_BurningAreaInfo;

    public void Enable(Vector3 pos, Vector3 direction, AttackInfo impactAttack, AttackInfo explosionAttack, AttackInfo burningAreaAttack)
    {
        m_ImpactAttack.Enable(pos, direction, impactAttack);
        GetComponent<Bullet>().OnImpact += OnImpact;

        m_ExplosionInfo = explosionAttack;
        m_BurningAreaInfo = burningAreaAttack;
    }

    void OnImpact(Bullet bullet, Enemy enemy)
    {
        // explode
        var explosionRadius = Services.Find<PlayerController>().CalculateRadius(m_ExplosionInfo.BaseArea);

        var ex = Instantiate(m_ExplosionEffect);
        ex.transform.position = transform.position;
        ex.transform.localScale = Vector3.one * explosionRadius;

        var targets = Physics2D.OverlapCircleAll(transform.position, explosionRadius, 1<<LayerMask.NameToLayer("Enemy"));
        foreach(var c in targets)
        {
            var e = c.GetComponent<Enemy>();
            if (e != null)
            {
                Services.Find<AttackController>().DoAttack(e, m_ExplosionInfo);
            } else
            {
                Debug.LogError(e);
            }
        }

        // burn
        var pos = bullet.transform.position;
        var p = Instantiate(m_BurningAreaAttack);
        p.Enable(pos, m_BurningAreaInfo);
    }
}
