using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnModel : ISpawnModel
{
    public Guid Id { get; } = Guid.NewGuid();
    public string Key { get; set; }
    public List<Guid> SpawnQueue { get; set; } = new();

    IReadOnlyList<Guid> ISpawnModel.SpawnQueue => SpawnQueue;
}
