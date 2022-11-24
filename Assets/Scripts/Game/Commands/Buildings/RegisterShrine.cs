using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegisterShrine : ICommand
{
    Guid _id;
    public RegisterShrine(Guid id)
    {
        _id = id;
    }

    public void Execute(GameModel model)
    {
        var shrine = new ShrineModel()
        {
            Id = _id,
            HasBlessingAvailable = true
        };
        model.Shrines.AddItem(shrine);
    }
}
