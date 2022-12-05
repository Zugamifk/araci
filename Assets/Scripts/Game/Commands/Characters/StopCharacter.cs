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
        var character = model.Characters.GetItem(_id);
        character.Movement.Mode = MoveMode.None;
        character.Movement.Speed = 0;
    }
}
