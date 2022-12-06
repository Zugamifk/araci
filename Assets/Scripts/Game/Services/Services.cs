using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Services
{
    static Dictionary<Type, IService> _typeToServiceLookup = new();

    public static void InitializeServices()
    {
        var serviceTypes = ReflectionCache.AllConcreteTypesOf<IService>();
        List<Type> _monobehaviourServiceTypes = new();

        foreach (var type in serviceTypes)
        {
            if (typeof(MonoBehaviour).IsAssignableFrom(type))
            {
                _monobehaviourServiceTypes.Add(type);
            }
            else
            {
                var instance = (IService)Activator.CreateInstance(type);
                AddService(instance);
            }
        }

        var root = GameObject.FindObjectOfType<ViewServiceManager>().transform;
        foreach (var type in _monobehaviourServiceTypes)
        {
            var instance = (IService)GameObject.FindObjectOfType(type);
            if (instance != null)
            {
                AddService(instance);
            }
            else
            {
                var go = new GameObject(type.Name);
                var service = (IService)go.AddComponent(type);
                go.transform.SetParent(root);
                AddService(service);
            }
        }
    }

    static void AddService(IService service)
    {
        var type = service.GetType();
        foreach (var i in type.GetInterfaces())
        {
            if (i != typeof(IService) && typeof(IService).IsAssignableFrom(i))
            {
                _typeToServiceLookup.Add(i, service);
            }
        }
    }

    public static TService Get<TService>()
        where TService : IService
    {
        return (TService)_typeToServiceLookup[typeof(TService)];
    }
}
