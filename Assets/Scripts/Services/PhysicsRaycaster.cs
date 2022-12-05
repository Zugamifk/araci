using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsRaycaster : MonoBehaviour, IPhysicsRaycaster
{
    void Start()
    {

    }

    public Vector2 Raycast(Vector2 gridPosition, Vector2 gridDirection)
    {
        var worldPosition = Map.Instance.GetWorldPosition(gridPosition);
        var worldDirection = Map.Instance.GetWorldPosition(gridDirection);
        var hit = Physics2D.Raycast(worldPosition, worldDirection);
        return hit.point;
    }
}
