using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovementModel : IIdentifiable
{
    Vector2 Position { get; }
}