using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateItemInInventory : CreateItemBase
{
    string key;
    int count;

    public CreateItemInInventory(string key, int count)
    {
        this.key = key;
        this.count = count;
    }   

    public override void Execute(GameModel model)
    {
        var item = CreateItemModel(key, count);
        model.Items.AddItem(item);
        new AddItemToInventory(item.Id).Execute(model);
    }
}
