using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateModel<TModel> : ICommand
    where TModel : IRegisteredModel, new()
{
    public void Execute(GameModel model)
    {
        model.CreateModel<TModel>();
        OnCreatedModel(model, model.GetModel<TModel>());
    }

    protected virtual void OnCreatedModel(GameModel game, TModel model)
    {

    }
}
