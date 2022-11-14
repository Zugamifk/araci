using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyToDataLookup<TData> : ScriptableObject
    where TData : ScriptableObject, IKeyHolder
{
    [SerializeField]
    TData[] _data;

    Dictionary<string, TData> _keyToDataLookup = new();

    private void OnEnable()
    {
        foreach(var data in _data)
        {
            _keyToDataLookup.Add(data.Key, data);
        }
    }

    public TData GetData(string key)
    {
        if(_keyToDataLookup.TryGetValue(key, out TData data))
        {
            return data;
        } else
        {
            return default;
        }
    }
}
