using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddLocation : ICommand
{
    string key;
    Vector2 position;

    public AddLocation(string key, Vector2 position)
    {
        this.key = key;
        this.position = position;
    }

    public void Execute(GameModel model)
    {
        model.MapLocations.Add(key, position);
    }
}
