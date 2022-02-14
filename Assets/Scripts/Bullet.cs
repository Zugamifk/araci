using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [NonSerialized]
    public int Damage;
    [NonSerialized]
    public float Speed;
    [NonSerialized]
    public Vector3 Direction;
    [NonSerialized]
    public float LifeTime;
    public OnImpactCallback OnImpact;

    public delegate void OnImpactCallback(Bullet bullet);

    private void Start()
    {
        var ang = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, ang-180);
    }

    private void Update()
    {
        LifeTime -= Time.deltaTime;
        if(LifeTime < 0)
        {
            Destroy(gameObject);
            OnImpact?.Invoke(this);
        }

        transform.position += Direction * Speed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var e = collision.gameObject.GetComponent<Enemy>();
        e.Health -= Damage;
        Debug.Log($"doing {Damage} damage");
        OnImpact?.Invoke(this);
        Destroy(gameObject);
    }
}
