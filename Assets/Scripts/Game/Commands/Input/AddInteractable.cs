using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct AddInteractable : ICommand
{
    Guid _id;
    Vector2 _position;

    public AddInteractable(Guid id, Vector2 position)
    {
        _id = id;
        _position = position;
    }

    public void Execute(GameModel model)
    {
        model.Input.InteractableTargets[_id] = new InteractableModel() { Id = _id, Position = _position };
    }
}
