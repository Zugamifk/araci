using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Prefabs
{
    static Dictionary<Type, IPrefabLookup> _nameableTypeToPrefabCollection = new Dictionary<Type, IPrefabLookup>();

    public static void Register<TKeyHolder>(IPrefabLookup lookup)
        where TKeyHolder : IKeyHolder
    {
        _nameableTypeToPrefabCollection[typeof(TKeyHolder)] = lookup;
    }

    public static GameObject GetInstance<TKeyHolder>(TKeyHolder key)
        where TKeyHolder : IKeyHolder
    {
        try
        {
            var data = _nameableTypeToPrefabCollection[typeof(TKeyHolder)];
            var prefab = data.GetPrefab(key.Key);
            return GameObject.Instantiate(prefab);
        } catch
        {
            Debug.LogError($"Failed to get prefab {key.Key} of type {typeof(TKeyHolder)}");
            throw;
        }
    }
}
