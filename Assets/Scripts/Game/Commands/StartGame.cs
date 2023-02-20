using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct StartGame : ICommand
{
    public void Execute(GameModel model)
    {
        new InitializePlayer().Execute(model);
        //new SpawnEnemy("Piper", "Test").Execute(model);
        //new StartNarrative("Test").Execute(model);
    }
}
