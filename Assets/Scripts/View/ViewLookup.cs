using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ViewLookup
{
    static Dictionary<Guid, GameObject> _idToView = new();

    public static void Register(Guid id, GameObject root)
    {
        _idToView.Add(id, root);
    }

    public static void Remove(Guid id)
    {
        _idToView.Remove(id);
    }

    public static GameObject Get(Guid id)
    {
        return _idToView.GetValueOrDefault(id);
    }
}
