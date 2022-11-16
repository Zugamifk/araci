using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegisterSpawn : ICommand
{
    string _key;
    Vector2[] _corners;

    public RegisterSpawn(string key, Vector2[] corners)
    {
        _key = key;
        _corners = corners;
    }

    public void Execute(GameModel model)
    {
        var spawn = new SpawnModel()
        {
            Key = _key,
            BoundingCorners = _corners
        };
        model.Spawns.Add(_key, spawn);
    }
}
