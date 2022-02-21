using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCrackerBurningArea : MonoBehaviour
{
    [SerializeField]
    AreaEffectParticles m_Particles;
    [SerializeField]
    AreaAttackTrigger m_Area;

    Dictionary<Enemy, float> m_EnemyToDamageinterval = new Dictionary<Enemy, float>();
    AttackInfo m_AttackInfo;

    public void Enable(Vector3 position, AttackInfo attack)
    {
        transform.position = position;

        var area = Services.Find<PlayerController>().CalculateRadius(attack.BaseArea);
        m_Particles.UpdateArea(area);

        m_Area.OnEnemyStay += OnEnemyStay;

        m_AttackInfo = attack;
    }

    void OnEnemyStay(IDamageable d)
    {
        var e = (Enemy)d;
        if (e == null) return;

        float t=0;
        if (!m_EnemyToDamageinterval.TryGetValue(e, out t))
        {
            m_EnemyToDamageinterval.Add(e, 0);
        }

        if (t <= 0)
        {
            var pc = Services.Find<PlayerController>();
            var dt = pc.CalculateAttackInterval(m_AttackInfo.BaseInterval);
            while(t <= 0)
            {
                Services.Find<AttackController>().DoAttack(d, m_AttackInfo);
                t += dt;
            }
            m_EnemyToDamageinterval[e] = t;
        }
    }
}
