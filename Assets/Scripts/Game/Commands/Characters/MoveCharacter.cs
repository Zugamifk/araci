using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct MoveCharacter : ICommand
{
    Guid _id;
    Vector2 _direction;
    Space _space;

    public MoveCharacter(Guid id, Vector2 direction, Space space)
    {
        _id = id;
        _direction = direction;
        _space = space;
    }

    public void Execute(GameModel model)
    {
        var move = model.Movement.GetItem(_id);
        var character = model.Characters.GetItem(_id);
        move.DesiredMove = _direction * character.MoveSpeed * model.TimeModel.LastDeltaTime;
        move.MovementSpace = _space;
    }
}
