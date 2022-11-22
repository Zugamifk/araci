using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackData : SimplePrefabReference
{
    [SerializeField]
    int _damage = 10;

    public int Damage => _damage;
}
