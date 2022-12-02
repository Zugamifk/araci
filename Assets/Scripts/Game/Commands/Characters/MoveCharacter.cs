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
    string _specialMoveAnim;

    public MoveCharacter(Guid id, Vector2 direction, Space space, float? speed = null, string specialMoveAnim = null)
    {
        _id = id;
        _direction = direction;
        _space = space;
        _speed = speed;
        _specialMoveAnim = specialMoveAnim;
    }

    public void Execute(GameModel model)
    {
        var character = model.Characters.GetItem(_id);
        var speed = _speed ?? character.MoveSpeed;
        character.Movement.DesiredMove = _direction * speed;
        if(_direction.sqrMagnitude > 0)
        {
            character.Movement.Direction = _direction;
        }
        character.Movement.MovementSpace = _space;
        character.Movement.SpecialMoveKey = _specialMoveAnim;
    }
}
