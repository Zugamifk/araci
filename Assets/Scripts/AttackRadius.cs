using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRadius : MonoBehaviour
{
    public float Radius;

    public Transform GetNearestTarget()
    {
        var pos = transform.position;
        var hits = Physics2D.OverlapCircleAll(pos, Radius, 1<<LayerMask.NameToLayer("Enemy"));
        Collider2D closest=null;
        var closestDistance = float.MaxValue;
        foreach(var h in hits)
        {
            var distance = ((Vector2)h.transform.position - (Vector2)pos).magnitude;
            if(distance < closestDistance)
            {
                closest = h;
                closestDistance = distance;
            }
        }
        if (closest != null)
        {
            return closest.transform;
        }
        else
        {
            return null;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Radius);
    }
}
