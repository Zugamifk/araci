using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CreateItemBase : ICommand
{
    public abstract void Execute(GameModel model);

    protected ItemModel CreateItemModel(string key, int count)
    {
        return new ItemModel()
        {
            Key = key,
            Count = count
        };
    }
}
