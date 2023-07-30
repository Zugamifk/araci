using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegisterShrine : ICommand
{
    Guid id;
    Vector2 worldPosition;
    Action<IShrineModel> onRegistered;
    public RegisterShrine(Guid id, Vector2 position, Action<IShrineModel> onRegistered)
    {
        this.id = id;
        this.worldPosition = position;
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

        var serv = Services.Get<ITileMapService>();
        var position = new PositionModel()
        {
            Id = id,
        };
        position.Position.Value = serv.WorldToGridSpace(worldPosition);
        model.Positions.AddItem(position);

        var interactionService = Services.Get<IInteractionService>();
        interactionService.RegisterInteractable<IShrineModel>(id);

        onRegistered?.Invoke(shrine);
    }
}
