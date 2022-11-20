using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterData : ScriptableObject, IPrefabReference
{
    [SerializeField]
    GameObject _prefab;
    [SerializeField]
    string _name;
    [SerializeField]
    float _moveSpeed = 5;

    public string Name => name;

    public GameObject Prefab => _prefab;

    public float MoveSpeed => _moveSpeed;
}
