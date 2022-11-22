using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SimplePrefabReference : ScriptableObject, IPrefabReference
{
    [SerializeField]
    GameObject _prefab;
    [SerializeField]
    string _name;

    public string Name => _name;

    public GameObject Prefab => _prefab;
}
