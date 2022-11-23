using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveCharacter : ICommand
{
    Guid _id;

    public RemoveCharacter(Guid id)
    {
        _id = id;
    }

    public void Execute(GameModel model)
    {
        model.Characters.RemoveItem(_id);
    }
}
