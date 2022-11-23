using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct GainExperience : ICommand
{
    int _experience;

    public GainExperience(int experience)
    {
        _experience = experience;
    }

    public void Execute(GameModel model)
    {
        var level = model.Player.Level;
        level.CurrentExperience += _experience;
        if(level.CurrentExperience > level.RequiredExperience)
        {
            new LevelUp().Execute(model);
        }
    }
}
