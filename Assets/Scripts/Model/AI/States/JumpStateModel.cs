using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpStateModel : TimedActionStateModel
{
    public Vector2 Direction { get; set; }
    public float Speed { get; set; }
}
