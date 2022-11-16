using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpawnModel : IIdentifiable, IKeyHolder
{
    IReadOnlyList<Guid> SpawnQueue { get; }
}
