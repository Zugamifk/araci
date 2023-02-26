using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct SetCharacterPosition : ICommand
{
    Guid _id;
    Vector2 _position;

    public SetCharacterPosition(Guid id, Vector2 position)
    {
        _id = id;
        _position = position;
    }

    public void Execute(GameModel model)
    {
        var character = model.Characters.GetItem(_id);
        character.Movement.Position = _position;
    }
}
