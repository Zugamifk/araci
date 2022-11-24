using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShrineModel : IIdentifiable
{
    bool HasBlessingAvailable { get; }
}
