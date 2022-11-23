using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct LevelUp : ICommand
{
    public void Execute(GameModel model)
    {
        var level = model.Player.Level;
        level.CurrentLevel++;
        var data = DataService.GetData<LevelDataCollection>().GetLevel(level.CurrentLevel);
        level.RequiredExperience = data.RequiredExperience;
    }
}
