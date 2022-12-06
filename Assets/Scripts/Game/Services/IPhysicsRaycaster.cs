using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPhysicsRaycaster : IService
{
    Vector2 Raycast(Vector2 gridPosition, Vector2 gridDirection, int layermask);
}
