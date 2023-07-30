using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHarvestableModel : IIdentifiable, IKeyHolder
{
    int HarvestCount { get; }
    IObservable<bool> IsHarvestable { get; }
}
