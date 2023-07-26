using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public struct KillCharacter : ICommand
{
    Guid _id;

    public KillCharacter(Guid id)
    {
        _id = id;
    }

    public void Execute(GameModel model)
    {
        var character = model.Characters.GetItem(_id);
        character.Health.IsAlive.Value = false;

        var data = DataService.GetData<CharacterDataCollection>().Get(character.Key);
        new GainExperience(data.ExperienceReward).Execute(model);

        // don't remove model -- need to play death effect which will remove model after playing
    }
}
