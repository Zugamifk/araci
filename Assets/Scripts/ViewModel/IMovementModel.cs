using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovementModel : IIdentifiable
{
    IObservable<float> Speed { get; }
    IObservable<Vector2> Direction { get; }
}
