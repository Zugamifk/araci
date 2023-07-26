using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICameraModel : IIdentifiable
{
    Guid TargetId { get; }
    IObservable<float> Size { get; }
}
