using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SimplePrefabReference : ScriptableObject, IPrefabReference
{
    [SerializeField]
    GameObject _prefab;
    [SerializeField]
    KeyAsset _key;

    public string Key => _key;

    public GameObject Prefab => _prefab;
}
