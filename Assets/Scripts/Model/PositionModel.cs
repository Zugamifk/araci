using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionModel : IPositionModel
{
    public Guid Id { get; set; }
    public Observable<Vector2> Position { get; } = new();
    IObservable<Vector2> IPositionModel.Position => Position;
}
