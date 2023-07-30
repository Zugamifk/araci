using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarvestableModel : IHarvestableModel
{
    public Guid Id { get; set; }
    public string Key { get; set; }
    public Observable<bool> IsHarvestable { get; } = new();
    public int HarvestCount { get; set; }
    IObservable<bool> IHarvestableModel.IsHarvestable => IsHarvestable;
}
