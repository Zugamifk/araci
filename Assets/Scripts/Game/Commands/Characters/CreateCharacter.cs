using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

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
        var positionModel = new PositionModel()
        {
            Id = id
        };
        positionModel.Position.Value = position;
        model.Positions.AddItem(positionModel);

        Game.AddUpdater(new PositionUpdater(id));

        var data = DataService.GetData<CharacterDataCollection>().Get(key);
        var movement = new MovementModel()
        {
            Id = id,
        };
        model.Movements.AddItem(movement);

        var character = new CharacterModel()
        {
            Id = id,
            Key = key,
            MoveSpeed = data.MoveSpeed
        };
        character.Health.CurrentHealth = data.HitPoints;
        character.Health.MaxHealth = data.HitPoints;
        character.Health.IsAlive.Value = true;
        model.Characters.AddItem(character);

        if (isUnique)
        {
            model.UniqueKeyToId.Add(key, id);
        }
    }
}
