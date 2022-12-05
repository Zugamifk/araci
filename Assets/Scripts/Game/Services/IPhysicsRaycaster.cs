using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPhysicsRaycaster : IService
{
    Vector2 Raycast(Vector2 position, Vector2 direction);
}
