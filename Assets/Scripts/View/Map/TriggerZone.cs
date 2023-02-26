using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public abstract class TriggerZone : MonoBehaviour
{
    [SerializeField]
    Bounds bounds;

    protected abstract Color BoundsColor { get; }

    protected virtual void OnEnable()
    {
        var collider = GetComponent<BoxCollider2D>();
        collider.size = bounds.size;
        collider.offset = bounds.center;
    }

    protected abstract void OnEnter();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnEnter();
    }


    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = BoundsColor;
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawWireCube(bounds.center, bounds.size);
    }
}
