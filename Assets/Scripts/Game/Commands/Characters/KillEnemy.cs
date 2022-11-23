using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public struct KillEnemy : ICommand
{
    Guid _id;

    public KillEnemy(Guid id)
    {
        _id = id;
    }

    public void Execute(GameModel model)
    {
        var character = model.Characters.GetItem(_id);
        var data = DataService.GetData<CharacterDataCollection>().Get(character.Key);
        new GainExperience(data.ExperienceReward).Execute(model);

        // don't remove model -- need to play death effect which will remove model after playing
    }
}
