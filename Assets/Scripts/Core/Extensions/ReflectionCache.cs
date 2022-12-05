using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;
using System.Linq;

public static class ReflectionCache
{
    static IEnumerable<Type> _allTypes;

    static ReflectionCache()
    {
        _allTypes = AppDomain.CurrentDomain.GetAssemblies().SelectMany(t => t.GetTypes());
    }

    public static IEnumerable<Type> AllTypes => _allTypes;

    public static IEnumerable<Type> AllTypesOf<TType>() => _allTypes.Where(t => typeof(TType).IsAssignableFrom(t));
    public static IEnumerable<Type> AllConcreteTypesOf<TType>() => AllTypesOf<TType>().Where(t => (t.IsClass || t.IsValueType) && !t.IsAbstract);
}
