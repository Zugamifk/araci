using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterData : SimplePrefabReference
{
    [SerializeField]
    float _moveSpeed = 5;
    [SerializeField]
    int _hitPoints = 10;

    public float MoveSpeed => _moveSpeed;
    public int HitPoints => _hitPoints;
}
