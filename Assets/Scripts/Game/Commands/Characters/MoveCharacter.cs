using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct MoveCharacter : ICommand
{
    Guid _id;
    Vector2 _direction;
    float? _speed;

    public MoveCharacter(Guid id, Vector2 direction, float? speed = null)
    {
        _id = id;
        _direction = direction;
        _speed = speed;
    }

    public void Execute(GameModel model)
    {
        var movement = model.Movements.GetItem(_id);
        if(movement == null)
        {
            throw new InvalidOperationException($"No movement model with id {_id}");
        }

        if(_direction.sqrMagnitude > 0)
        {
            var character = model.Characters.GetItem(_id);
            movement.Speed = _speed ?? character.MoveSpeed;
            movement.Direction = _direction;
        } else
        {
            movement.Speed = 0;
        }
    }
}
