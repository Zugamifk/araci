using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DataService
{
    static Dictionary<System.Type, ScriptableObject> _dataTypeToCollection = new Dictionary<System.Type, ScriptableObject>();

    internal static void Register<T>(T collection) where T : ScriptableObject
    {
        if(collection is not IRegisteredData)
        {
            throw new System.InvalidOperationException($"Can't register {collection} of type {typeof(T)}! Must inherit from IRegisteredData.");
        }

        _dataTypeToCollection[collection.GetType()] = collection;
    }

    internal static void Clear()
    {
        _dataTypeToCollection.Clear();
    }

    public static T GetData<T>() where T : ScriptableObject, IRegisteredData
    {
        return (T)_dataTypeToCollection[typeof(T)];
    }
}
