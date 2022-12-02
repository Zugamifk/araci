using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovementModel
{
    Vector2 Position { get; }
    Vector2 Direction { get; }
    Vector2 DesiredMove { get; }
    Space MovementSpace { get; }
    string SpecialMoveKey { get; }

}
