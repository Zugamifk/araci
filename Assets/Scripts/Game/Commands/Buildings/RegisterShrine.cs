using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegisterShrine : ICommand
{
    Guid id;
    Action<IShrineModel> onRegistered;
    public RegisterShrine(Guid id, Action<IShrineModel> onRegistered)
    {
        this.id = id;
        this.onRegistered = onRegistered;   
    }

    public void Execute(GameModel model)
    {
        var shrine = new ShrineModel()
        {
            Id = id,
        };
        shrine.HasBlessingAvailable.Value = true;
        model.Shrines.AddItem(shrine);

        onRegistered?.Invoke(shrine);
    }
}
