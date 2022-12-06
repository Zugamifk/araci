using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsRaycaster : MonoBehaviour, IPhysicsRaycaster
{
    public Vector2 Raycast(Vector2 gridPosition, Vector2 gridDirection, int layermask)
    {
        var worldPosition = Map.Instance.GridToWorldSpace(gridPosition);
        var worldDirection = Map.Instance.GridToWorldSpace(gridDirection);
        var hit = Physics2D.Raycast(worldPosition, worldDirection, float.MaxValue, layermask);
        return Map.Instance.WorldToGridSpace(hit.point);
    }
}
