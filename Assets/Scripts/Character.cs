using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField]
    AttackRadius m_AttackRadiusTemplate;

    float m_MoveSpeed;

    List<Enemy> m_Touching = new List<Enemy>();
    Rigidbody2D m_Rigidbody;

    public IEnumerable<Enemy> TouchingEnemies => m_Touching;

    Dictionary<Item, AttackRadius> m_AttackRadiuses = new Dictionary<Item, AttackRadius>();

    private void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var dir = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"),0);
        m_Rigidbody.MovePosition(transform.position + dir * m_MoveSpeed * Time.fixedDeltaTime);
    }

    public void SetMoveSpeed(float speed)
    {
        m_MoveSpeed = speed;
    }

    public AttackRadius GetAttackRadius(Item item)
    {
        AttackRadius radius;
        if (!m_AttackRadiuses.TryGetValue(item, out radius))
        {
            radius = Instantiate(m_AttackRadiusTemplate);
            radius.transform.SetParent(transform, false);
            radius.transform.localPosition = Vector3.zero;
            radius.gameObject.SetActive(true);
            m_AttackRadiuses.Add(item, radius);
        }
        return radius;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            m_Touching.Add(enemy);
        }

        var pickup = collision.gameObject.GetComponent<Pickup>();
        if (pickup != null)
        {
            pickup.PickupItem();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        var enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            m_Touching.Remove(enemy);
        }
    }
}
