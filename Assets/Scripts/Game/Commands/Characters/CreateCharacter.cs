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
        var data = DataService.GetData<CharacterDataCollection>().Get(_key);
        var character = new CharacterModel()
        {
            Id = _id,
            Key = _key,
            MoveSpeed = data.MoveSpeed
        };
        character.Movement.Position = _position;
        character.Health.HitPoints = data.HitPoints;
        model.Characters.AddItem(character);
    }
}
