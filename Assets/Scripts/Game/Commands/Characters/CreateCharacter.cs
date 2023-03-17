using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCharacter : ICommand
{

    Guid id;
    string key;
    Vector2 position;
    bool isUnique;

    public CreateCharacter(Guid id, string key, Vector2 position, bool isUnique = false)
    {
        this.id = id;
        this.key = key;
        this.position = position;
        this.isUnique = isUnique;
    }

    public void Execute(GameModel model)
    {
        var data = DataService.GetData<CharacterDataCollection>().Get(key);
        var character = new CharacterModel()
        {
            Id = id,
            Key = key,
            MoveSpeed = data.MoveSpeed
        };
        character.Movement.Position = position;
        character.Health.CurrentHealth = data.HitPoints;
        character.Health.MaxHealth = data.HitPoints;
        model.Characters.AddItem(character);

        if (isUnique)
        {
            model.UniqueKeyToId.Add(key, id);
        }
    }
}
