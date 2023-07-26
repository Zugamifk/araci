using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovementModel : IIdentifiable
{
    float Speed { get; }
    Vector2 Direction { get; }
}
