using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementModel : IMovementModel
{
    public Vector2 Position { get; set; }
    public Vector2 Direction { get; set; }
    public Vector2 DesiredMove { get; set; }
    public Space MovementSpace { get; set; }
}
