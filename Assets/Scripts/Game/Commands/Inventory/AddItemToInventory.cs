using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddItemToInventory : ICommand
{
    Guid id;

    public AddItemToInventory(Guid id)
    {
        this.id = id;
    }   

    public void Execute(GameModel model)
    {
        model.Inventory.Slots.Add(id);
    }
}
