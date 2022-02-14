using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area : MonoBehaviour
{
    BoxCollider2D m_Collider;
    private void Awake()
    {
        m_Collider = GetComponent<BoxCollider2D>();
    }

    public Vector3 GetRandomPosition()
    {
        var p = transform.position;
        var b = m_Collider.bounds;
        return p + new Vector3(Random.Range(b.min.x, b.max.x), Random.Range(b.min.y, b.max.y), p.z);
    }
}
