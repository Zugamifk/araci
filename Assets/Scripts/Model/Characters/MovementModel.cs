using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementModel : IMovementModel
{
    public Guid Id { get; set; }
    public Observable<Vector2> Direction { get; } = new();
    public Observable<float> Speed { get; } = new();
    IObservable<float> IMovementModel.Speed => Speed;
    IObservable<Vector2> IMovementModel.Direction => Direction;
}
