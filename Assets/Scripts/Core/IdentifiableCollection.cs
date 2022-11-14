using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class IdentifiableCollection<TModel> : IIdentifiableLookup<TModel>
    where TModel : IIdentifiable
{
    Dictionary<Guid, TModel> _identifiables = new Dictionary<Guid, TModel>();

    public IEnumerable<TModel> AllItems => _identifiables.Values;

    public TModel this[Guid id] => GetItem(id);
    public bool IsEmpty => _identifiables.Count == 0;

    public void AddItem(TModel model)
    {
        _identifiables[model.Id] = model;
    }

    public void RemoveItem(Guid id)
    {
        _identifiables.Remove(id);
    }

    public TModel GetItem(Guid id)
    {
        if(_identifiables.TryGetValue(id, out TModel value))
        {
            return value;
        } else
        {
            return default;
        }
    }

    public bool HasId(Guid id)
    {
        return _identifiables.ContainsKey(id);
    }

}
