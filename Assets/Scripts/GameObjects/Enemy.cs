using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField]
    Transform m_ViewRoot;

    [SerializeField]
    int m_Health = 5;
    
    [SerializeField]
    int m_Damage;

    [SerializeField]
    float m_MoveSpeed;

    Character m_Player;
    Rigidbody2D m_RigidBody;

    public int Health => m_Health;
    public int Damage => m_Damage;

    private void Start()
    {
        m_Player = GameObject.FindObjectOfType<Character>();
        m_RigidBody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        var dir =  m_Player.transform.position - transform.position;
        dir.Normalize();
        if (dir.x != 0)
        {
            var a = Mathf.Sign(dir.x) < 0 ? 0 : 180;
            m_ViewRoot.transform.localRotation = Quaternion.Euler(0, a, 0);
        }
        m_RigidBody.MovePosition(transform.position + dir * m_MoveSpeed * Time.fixedDeltaTime);
    }

    public void DoDamage(int damage)
    {
        m_Health -= damage;
        if (Health < 0)
        {
            Destroy(gameObject);
            Services.Find<GameController>().SpawnExperienceGem(transform.position);
        }
    }
}
