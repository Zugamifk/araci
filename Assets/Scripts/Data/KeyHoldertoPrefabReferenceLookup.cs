using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class KeyHoldertoPrefabReferenceLookup<TKeyHolder, TReference> : SimpleDataCollection<TReference>, IPrefabLookup, IRegisteredData
    where TKeyHolder : IKeyHolder
    where TReference : IPrefabReference
{
    public GameObject GetPrefab(string key) => Get(key).Prefab;

    protected override void OnEnable()
    {
        Prefabs.Register<TKeyHolder>(this);
        base.OnEnable();
    }
}
