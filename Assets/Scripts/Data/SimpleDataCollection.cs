using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SimpleDataCollection<TData> : ScriptableObject
    where TData : IKeyHolder
{
    [SerializeField]
    TData[] _data;

    Dictionary<string, TData> _keyToData = new Dictionary<string, TData>();

    public TData Get(string key) => _keyToData[key];
    public TData this[string key] => _keyToData[key];

    protected virtual void OnEnable()
    {
        foreach (var p in _data)
        {
            if (p != null)
            {
                _keyToData.Add(p.Key, p);
            }
            else
            {
                throw new System.InvalidOperationException($"Null in {name}!");
            }
        }
    }
}
