using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraModel : ICameraModel
{
    public Guid Id { get; } = Guid.NewGuid();
    public Guid TargetId { get; set; }
    public Observable<float> Size { get; set; } = new();
    IObservable<float> ICameraModel.Size => Size;
}
