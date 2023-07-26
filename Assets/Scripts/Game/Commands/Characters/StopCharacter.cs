using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopCharacter : ICommand
{
    Guid _id;

    public StopCharacter(Guid id)
    {
        _id = id;
    }

    public void Execute(GameModel model)
    {
        var character = model.Movements.GetItem(_id);
        character.Speed = 0;
    }
}
