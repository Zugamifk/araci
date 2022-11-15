using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCharacter : ICommand
{

    Guid _id;
    string _key;
    Vector2 _position;

    public CreateCharacter(Guid id, string key, Vector2 position)
    {
        _id = id;
        _key = key;
        _position = position;
    }

    public void Execute(GameModel model)
    {
        var movement = new MovementModel()
        {
            Id = _id,
            Position = _position
        };
        model.Movement.AddItem(movement);

        var character = new CharacterModel()
        {
            Id = _id,
            Key = _key,
        };
        model.Characters.AddItem(character);
    }
}
