using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCrackerAttack : MonoBehaviour
{
    [SerializeField]
    Attack m_ImpactAttack;
    [SerializeField]
    Attack m_ExplosionAttack;

    public void Enable(Vector3 pos, Vector3 direction, AttackInfo impactAttack, AttackInfo explosionAttack)
    {
        m_ImpactAttack.Spawn(pos, direction, impactAttack);
        GetComponent<Bullet>().OnImpact += OnImpact;

        m_ExplosionAttack.SetAttack(explosionAttack);
    }

    void OnImpact(Bullet bullet, Enemy enemy)
    {
        m_ExplosionAttack.enabled = true;
    }
}
