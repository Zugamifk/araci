using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct MoveCharacter : ICommand
{
    Guid _id;
    Vector2 _direction;
    Space _space;
    float? _speed;

    public MoveCharacter(Guid id, Vector2 direction, Space space, float? speed = null)
    {
        _id = id;
        _direction = direction;
        _space = space;
        _speed = speed;
    }

    public void Execute(GameModel model)
    {
        var character = model.Characters.GetItem(_id);
        if(character == null)
        {
            throw new InvalidOperationException($"No character with id {_id}");
        }

        if(_direction.sqrMagnitude > 0)
        {
            character.Movement.Speed = _speed ?? character.MoveSpeed;
            character.Movement.Direction = _direction;
            character.Movement.Mode = MoveMode.Direction;
        } else
        {
            character.Movement.Speed = 0;
            character.Movement.Mode = MoveMode.None;
        }

        character.Movement.MovementSpace = _space;
    }
}
