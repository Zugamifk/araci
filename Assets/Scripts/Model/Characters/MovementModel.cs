using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementModel : IMovementModel
{
    public MoveMode Mode { get; set; } = MoveMode.None;
    public Vector2 Position { get; set; }
    public Vector2 Direction { get; set; } = new Vector2(1, 0);
    public float Speed { get; set; }
    public Vector2 Destination { get; set; }
    public Space MovementSpace { get; set; }
}
