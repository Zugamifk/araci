using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SimplePrefabLookup;

public abstract class SimpleDataCollection<TKeyHolder, TReference> : KeyHoldertoPrefabReferenceLookup<TKeyHolder, TReference>
    where TKeyHolder : IKeyHolder
    where TReference : IPrefabReference
{
    [SerializeField]
    TReference[] _dataReferences;

    protected override IEnumerable<TReference> PrefabReferences => _dataReferences;
}
