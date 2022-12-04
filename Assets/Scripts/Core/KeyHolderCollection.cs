using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyHolderCollection<TModel>
    where TModel : IKeyHolder
{
    Dictionary<string, TModel> _identifiables = new Dictionary<string, TModel>();

    public IEnumerable<TModel> AllItems => _identifiables.Values;

    public TModel this[string key] => GetItem(key);
    public bool IsEmpty => _identifiables.Count == 0;

    public void AddItem(TModel model)
    {
        _identifiables[model.Key] = model;
    }

    public void RemoveItem(string key)
    {
        _identifiables.Remove(key);
    }

    public TModel GetItem(string key)
    {
        if (_identifiables.TryGetValue(key, out TModel value))
        {
            return value;
        }
        else
        {
            return default;
        }
    }

    public bool HasKey(string key)
    {
        return _identifiables.ContainsKey(key);
    }
}
