using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct StartGame : ICommand
{
    public void Execute(GameModel model)
    {
        new LevelUp(0).Execute(model);
        new CreateCharacter(model.Player.Id, "Player", new Vector2(5, 10)).Execute(model);
        new SpawnEnemy("Enemy", "Test").Execute(model);
    }
}
