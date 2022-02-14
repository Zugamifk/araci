using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceGem : Pickup
{
    public int Value;

    Transform m_PlayerTarget;
    float m_VaccuumSpeed;

    Rigidbody2D m_Rigidbody;

    private void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (m_PlayerTarget != null)
        {
            var dir = m_PlayerTarget.position - transform.position;
            dir.Normalize();

            m_Rigidbody.MovePosition(transform.position + dir * m_VaccuumSpeed * Time.fixedDeltaTime);
        }
    }

    public override void PickupItem()
    {
        Services.Find<GameController>().GainExperience(Value);
        Destroy(gameObject);
    }

    public void StartVaccuuming(Transform playerTransform, float speed)
    {
        m_VaccuumSpeed = speed;
        m_PlayerTarget = playerTransform;
    }
}
