using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class UseShrine : ICommand
{
    Guid id;

    public UseShrine(Guid id)
    {
        this.id = id;
    }

    public void Execute(GameModel model)
    {
        var shrine = model.Shrines.GetItem(id);
        shrine.HasBlessingAvailable.Value = false;
    }
}
