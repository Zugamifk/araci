using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICameraModel : IIdentifiable
{
    IObservable<float> Size { get; }
}
