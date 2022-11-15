using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct MoveCharacter : ICommand
{
    Guid _id;
    Vector2 _direction;

    public MoveCharacter(Guid id, Vector2 direction)
    {
        _id = id;
        _direction = direction;
    }

    public void Execute(GameModel model)
    {
        var move = model.Movement.GetItem(_id);
        var character = model.Characters.GetItem(_id);
        move.Position += _direction * character.MoveSpeed * model.TimeModel.LastDeltaTime;
    }
}
