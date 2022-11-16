using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCharacter : ICommand
{
    string _characterKey;
    string _spawnKey;

    public SpawnCharacter(string characterKey, string spawnKey)
    {
        _characterKey = characterKey;
        _spawnKey = spawnKey;
    }

    public void Execute(GameModel model)
    {
        var spawn = model.Spawns[_spawnKey];

    }
}
