using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterData : SimplePrefabReference
{
    [SerializeField]
    float _moveSpeed = 5;

    public float MoveSpeed => _moveSpeed;
}
