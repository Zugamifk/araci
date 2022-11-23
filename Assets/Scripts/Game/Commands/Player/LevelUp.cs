using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct LevelUp : ICommand
{
    int _level;

    public LevelUp(int level) { _level = level; }

    public void Execute(GameModel model)
    {
        var data = DataService.GetData<LevelDataCollection>().GetLevel(_level);
        var level = model.Player.Level;
        level.CurrentLevel = _level + 1;
        level.RequiredExperience = data.RequiredExperience;
    }
}
