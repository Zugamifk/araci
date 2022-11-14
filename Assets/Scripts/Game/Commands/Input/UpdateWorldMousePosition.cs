using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct UpdateWorldMousePosition : ICommand
{
    Guid _id;
    Vector3 _position;
    public UpdateWorldMousePosition(Guid id, Vector3 position)
    {
        _id = id;
        _position = position;
    }

    public void Execute(GameModel model)
    {
        model.Input.CurrentMouseOverObject = _id;
        model.Input.ClickPosition = _position;
    }
}
