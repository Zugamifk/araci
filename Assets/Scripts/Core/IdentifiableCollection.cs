using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class IdentifiableCollection<TModel> : IIdentifiableLookup<TModel>, IDisposable
    where TModel : IIdentifiable
{
    Dictionary<Guid, TModel> _identifiables = new Dictionary<Guid, TModel>();
    private bool disposedValue;

    public event Action<TModel> AddedItem;
    public event Action<TModel> RemovedItem;

    public IEnumerable<TModel> AllItems => _identifiables.Values;

    public TModel this[Guid id] => GetItem(id);
    public bool IsEmpty => _identifiables.Count == 0;

    public void AddItem(TModel model)
    {
        _identifiables[model.Id] = model;
        AddedItem?.Invoke(model);
    }

    public void RemoveItem(Guid id)
    {
        if(_identifiables.TryGetValue(id, out var item))
        {
            _identifiables.Remove(id);
            RemovedItem?.Invoke(item);
        }
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

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                foreach(var value in _identifiables.Values)
                {
                    RemovedItem?.Invoke(value);
                }
            }
            disposedValue = true;
        }
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
