using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCharacter : ICommand
{
    string characterKey;
    string spawnKey;
    Guid id;
    bool isUnique;

    public SpawnCharacter(string characterKey, string spawnKey, Guid id = default, bool isUnique = false)
    {
        this.characterKey = characterKey;
        this.spawnKey = spawnKey;
        this.id = id == default ? Guid.NewGuid() : id;
        this.isUnique = isUnique;
    }

    public void Execute(GameModel model)
    {
        if(!model.Spawns.ContainsKey(spawnKey))
        {
            throw new InvalidOperationException($"No spawn with key \'{spawnKey}\'!");
        }
        var spawn = model.Spawns[spawnKey];
        var c = spawn.BoundingCorners;
        var pos = c[0] + (c[1] - c[0]) * UnityEngine.Random.value + (c[3] - c[0]) * UnityEngine.Random.value;

        new CreateCharacter(id, characterKey, pos).Execute(model);
    }
}
