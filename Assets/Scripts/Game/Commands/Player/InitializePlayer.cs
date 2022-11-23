using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct InitializePlayer : ICommand
{
    public void Execute(GameModel model)
    {
        new CreateCharacter(model.Player.Id, "Player", new Vector2(5, 10)).Execute(model);

        var level = model.Player.Level;
        level.CurrentLevel=0;
        var data = DataService.GetData<LevelDataCollection>().GetLevel(0);
        level.RequiredExperience = data.RequiredExperience;
    }
}
