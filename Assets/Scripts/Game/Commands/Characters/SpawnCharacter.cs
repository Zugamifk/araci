using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCharacter : ICommand
{
    string _characterKey;
    string _spawnKey;
    Guid _id;

    public SpawnCharacter(string characterKey, string spawnKey, Guid id = default)
    {
        _characterKey = characterKey;
        _spawnKey = spawnKey;
        _id = id == default ? Guid.NewGuid() : id;
    }

    public void Execute(GameModel model)
    {
        var spawn = model.Spawns[_spawnKey];
        var c = spawn.BoundingCorners;
        var pos = c[0] + (c[1] - c[0]) * UnityEngine.Random.value + (c[3] - c[0]) * UnityEngine.Random.value;

        new CreateCharacter(_id, _characterKey, pos).Execute(model);
    }
}
