using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePrefabLookup : ScriptableObject
{
    [System.Serializable]
    public class Reference : IPrefabReference
    {
        [field: SerializeField]
        public string Name { get; set; }
        [field: SerializeField]
        public GameObject Prefab { get; set; }
    }

    [SerializeField]
    Reference[] _prefabs;

    Dictionary<string, GameObject> _nameToPrefab = new Dictionary<string, GameObject>();

    public GameObject GetItem(string key) => _nameToPrefab[key];

    private void OnEnable()
    {
        foreach (var p in _prefabs)
        {
            if (p != null)
            {
                _nameToPrefab.Add(p.Name, p.Prefab);
            }
            else
            {
                throw new System.InvalidOperationException("Null in BuildingCollection!");
            }
        }
    }
}
