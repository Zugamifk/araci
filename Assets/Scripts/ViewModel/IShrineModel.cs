using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShrineModel : IIdentifiable
{
    IObservable<bool> HasBlessingAvailable { get; }
}
