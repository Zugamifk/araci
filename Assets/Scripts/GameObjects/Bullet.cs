using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    Animator m_Animator;
    [SerializeField]
    float m_MaxLifeTime;

    float m_Speed;
    float m_LifeTime;

    public event OnImpactCallback OnImpact;

    public delegate void OnImpactCallback(Bullet bullet, Enemy enemy);

    public void Enable(float speed)
    {
        m_Speed = speed;
        m_LifeTime = m_MaxLifeTime;
    }

    private void Update()
    {
        m_LifeTime -= Time.deltaTime;
        if(m_LifeTime < 0)
        {
            OnImpact?.Invoke(this, null);
            Destroy(gameObject);
        }

        transform.position += transform.up * m_Speed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.otherCollider.enabled = false;
        var e = collision.gameObject.GetComponent<Enemy>();
        OnImpact?.Invoke(this, e);

        Destroy();
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
}
