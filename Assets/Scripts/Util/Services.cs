using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Services
{
    static Dictionary<Type, object> s_ServiceLookup = new Dictionary<Type, object>();

    public static T Find<T>() where T : class, new()
    {
        object result;
        if(!s_ServiceLookup.TryGetValue(typeof(T), out result))
        {
            if(typeof(MonoBehaviour).IsAssignableFrom(typeof(T)))
            {
                result = GameObject.FindObjectOfType(typeof(T));
            } else
            {
                result = new T();
            }

            s_ServiceLookup[typeof(T)] = result;
        }

        return (T)result;
    }
}
