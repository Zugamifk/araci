using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class KeyHoldertoPrefabReferenceLookup<TKeyHolder, TReference> : ScriptableObject, IPrefabLookup, IRegisteredData
    where TKeyHolder : IKeyHolder
    where TReference : IPrefabReference
{
    Dictionary<string, TReference> _nameToPrefab = new Dictionary<string, TReference>();

    public GameObject GetPrefab(string name) => _nameToPrefab[name].Prefab;
    public TReference this[string name] => _nameToPrefab[name];

    protected virtual void OnEnable()
    {
        foreach (var p in PrefabReferences)
        {
            if (p != null)
            {
                _nameToPrefab.Add(p.Name, p);
            }
            else
            {
                throw new System.InvalidOperationException("Null in BuildingCollection!");
            }
        }
        Prefabs.Register<TKeyHolder>(this);
    }

    protected abstract IEnumerable<TReference> PrefabReferences { get; }
}
