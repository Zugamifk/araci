using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct UseInteractable : ICommand
{
    Guid _id;

    public UseInteractable(Guid id)
    {
        _id = id;
    }

    public void Execute(GameModel model)
    {
        if (TryInteractWithShrine(model)) return;
    }

    bool TryInteractWithShrine(GameModel model)
    {
        var shrine = model.Shrines.GetItem(_id);
        if (shrine == null)
        {
            return false;
        }

        shrine.HasBlessingAvailable = false;

        return true;
    }
}
