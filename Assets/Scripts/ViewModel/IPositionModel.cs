using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPositionModel : IIdentifiable
{
    IObservable<Vector2> Position { get; }
}
