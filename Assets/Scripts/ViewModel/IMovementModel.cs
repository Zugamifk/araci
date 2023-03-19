using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovementModel
{
    float Speed { get; }
    Vector2 Position { get; }
    Vector2 Direction { get; }
}
