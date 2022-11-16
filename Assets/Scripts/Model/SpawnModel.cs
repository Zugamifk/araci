using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnModel : ISpawnModel
{
    public Guid Id { get; set; }
    public string Key { get; set; }
    public List<Guid> SpawnQueue { get; set; } = new();
    public Vector2[] BoundingCorners { get; set; }

    IReadOnlyList<Guid> ISpawnModel.SpawnQueue => SpawnQueue;
}
