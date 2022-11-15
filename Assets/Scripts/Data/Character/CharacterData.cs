using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterData : ScriptableObject, IPrefabReference
{
    [SerializeField]
    GameObject _prefab;

    public string Name => name;

    public GameObject Prefab => _prefab;
}
