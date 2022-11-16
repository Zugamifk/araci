using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct StartGame : ICommand
{
    public void Execute(GameModel model)
    {
        new CreateCharacter(model.Player.Id, "Player", new Vector2(5, 10)).Execute(model);
        new SpawnCharacter("Enemy", "Test").Execute(model);
    }
}
