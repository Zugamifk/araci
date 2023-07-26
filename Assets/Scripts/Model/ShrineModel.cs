using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrineModel : IShrineModel
{
    public Guid Id { get; set; }
    public Observable<bool> HasBlessingAvailable { get; } = new();
    IObservable<bool> IShrineModel.HasBlessingAvailable => HasBlessingAvailable;
}
