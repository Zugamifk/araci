using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovementModel
{
    MoveMode Mode
    {
        get;
    }
    float Speed { get; }
    Vector2 Position { get; }
    Vector2 Direction { get; }
}
