using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegisterHarvestable : ICommand
{
    Guid id;
    string key;
    Vector2 worldPosition;
    Action<IHarvestableModel> onRegistered;

    public RegisterHarvestable(Guid id, string key, Vector2 worldPosition, Action<IHarvestableModel> onRegistered)
    {
        this.id = id;
        this.key = key;
        this.worldPosition = worldPosition;
        this.onRegistered = onRegistered;
    }

    public void Execute(GameModel model)
    {
        var harvestable = new HarvestableModel()
        {
            Id = id,
            Key = key,
            HarvestCount = 5
        };
        harvestable.IsHarvestable.Value = true;
        model.Harvestables.AddItem(harvestable);

        var serv = Services.Get<ITileMapService>();
        var position = new PositionModel()
        {
            Id = id,
        };
        position.Position.Value = serv.WorldToGridSpace(worldPosition);
        model.Positions.AddItem(position);

        var interactionService = Services.Get<IInteractionService>();
        interactionService.RegisterInteractable<IHarvestableModel>(id);

        onRegistered?.Invoke(harvestable);
    }
}
