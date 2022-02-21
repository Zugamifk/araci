using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaAttackTrigger : MonoBehaviour
{
    public event Action<IDamageable> OnEnemyEnter;
    public event Action<IDamageable> OnEnemyStay;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var e = collision.GetComponent<Enemy>();
        if (e != null)
        {
            OnEnemyEnter?.Invoke(e);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        var e = collision.GetComponent<Enemy>();
        if (e != null)
        {
            OnEnemyStay?.Invoke(e);
        }
    }
}
