using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponData : ScriptableObject
{
    [SerializeField]
    string _key;
    [SerializeField]
    string _displayName;
    [SerializeField]
    string _description;
    [SerializeField]
    SkillData _startingSkill;
    [SerializeField]
    float _baseDamage;
    [SerializeField]
    float _baseMeterPerHit;
    [SerializeField]
    LevelDataCollection _levelData;

}
