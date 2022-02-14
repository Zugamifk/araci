using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int Health = 5;
    public int Damage;
    public float MoveSpeed;

    Character m_Player;
    Rigidbody2D m_RigidBody;

    private void Start()
    {
        m_Player = GameObject.FindObjectOfType<Character>();
        m_RigidBody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        var dir =  m_Player.transform.position - transform.position;
        dir.Normalize();
        m_RigidBody.MovePosition(transform.position + dir * MoveSpeed * Time.fixedDeltaTime);
    }

    private void Update()
    {
        if (Health < 0)
        {
            Destroy(gameObject);
            Services.Find<GameController>().SpawnExperienceGem(transform.position);
        }
    }
}
