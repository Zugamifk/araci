using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Identifiable))]
public abstract class ModelViewBase<TModel> : MonoBehaviour, IModelView<TModel>
    where TModel : IIdentifiable
{
    Identifiable _identifiable;

    public Guid Id => _identifiable.Id;

    protected virtual void Awake()
    {
        _identifiable = GetComponent<Identifiable>();
    }

    public abstract TModel GetModel();

    public abstract void InitializeFromModel(TModel model);
}
