using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Services
{
    static Dictionary<Type, IService> _typeToServiceLookup = new();

    static List<Type> _monobehaviourServiceTypes = new();

    static Services()
    {
        var serviceTypes = ReflectionCache.AllConcreteTypesOf<IService>();
        foreach(var type in serviceTypes)
        {
            if (typeof(MonoBehaviour).IsAssignableFrom(type))
            {
                _monobehaviourServiceTypes.Add(type);
            } else
            {
                var instance = (IService)Activator.CreateInstance(type);
                _typeToServiceLookup.Add(type, instance);
            }
        }
    }

    public static void InitializeViewServices()
    {
        var root = GameObject.FindObjectOfType<ViewServiceManager>().transform;
        foreach(var type in _monobehaviourServiceTypes)
        {
            var go = new GameObject(type.Name);
            var service = (IService)go.AddComponent(type);
            go.transform.SetParent(root);
            _typeToServiceLookup.Add(type, service);
        }
    }

    public static TService Get<TService>()
        where TService : IService
    {
        return (TService)_typeToServiceLookup[typeof(TService)];
    }
}
