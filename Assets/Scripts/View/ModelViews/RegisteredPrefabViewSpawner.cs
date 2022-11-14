using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class RegisteredPrefabViewSpawner<TModel, TView> : ViewSpawner<TModel, TView>
    where TModel : IIdentifiable, IKeyHolder
    where TView : MonoBehaviour, IModelView<TModel>
{
    protected sealed override GameObject InstantiateView(TModel model)
    {
        return Prefabs.GetInstance(model);
    }
}
