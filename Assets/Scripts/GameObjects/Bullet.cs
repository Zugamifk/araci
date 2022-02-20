using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Attack
{
    [SerializeField]
    Animator m_Animator;
    [SerializeField]
    string m_ImpactAnimationTrigger;

    [NonSerialized]
    public float Speed;
    [NonSerialized]
    public float LifeTime;

    public OnImpactCallback OnImpact;

    public AttackInfo Attack;

    public delegate void OnImpactCallback(Bullet bullet);

    private void Update()
    {
        LifeTime -= Time.deltaTime;
        if(LifeTime < 0)
        {
            Destroy(gameObject);
            OnImpact?.Invoke(this);
        }

        transform.position += transform.up * Speed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var e = collision.gameObject.GetComponent<Enemy>();
        Services.Find<AttackController>().DoAttack(e, Attack);
        OnImpact?.Invoke(this);

        if (m_Animator != null)
        {
            m_Animator.SetTrigger(m_ImpactAnimationTrigger);
            Speed = 0;
        } else { 
            Destroy(gameObject);
        }
    }
}
