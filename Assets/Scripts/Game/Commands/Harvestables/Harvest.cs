using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harvest : ICommand
{
    Guid id;
    public Harvest(Guid id)
    {
        this.id = id;
    }

    public void Execute(GameModel model)
    {
        var harvestable = model.Harvestables[id];
        harvestable.IsHarvestable.Value = false;
        Game.Do(new CreateItemInInventory(harvestable.Key, harvestable.HarvestCount));
    }
}
