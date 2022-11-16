using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovementModel : IIdentifiable
{
    Vector2 Position { get; }
    Vector2 DesiredMove { get; }
    Space MovementSpace { get; }

}
