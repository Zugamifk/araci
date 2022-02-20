using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaccuumRadius : MonoBehaviour
{
    [SerializeField]
    float m_VaccuumSpeed;

    List<Rigidbody2D> m_Vaccuuming = new List<Rigidbody2D>();

    private void FixedUpdate()
    {
        m_Vaccuuming.RemoveAll(v => v == null);

        foreach (var v in m_Vaccuuming)
        {
            var vp = v.transform.position;
            var dir = transform.position - vp;
            dir.Normalize();

            v.MovePosition(vp + dir * m_VaccuumSpeed * Time.fixedDeltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var pickup = collision.gameObject.GetComponent<Pickup>();
        if(pickup!=null)
        {
            m_Vaccuuming.Add(pickup.GetComponent<Rigidbody2D>());
        }
    }
}
