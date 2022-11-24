using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponData : ScriptableObject, IKeyHolder
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
    float _baseAttackTime;
    [SerializeField]
    LevelDataCollection _levelData;

    public string Key => _key;
    public string DisplayName => _displayName;

    public string Description => _description;
    public float BaseDamage => _baseDamage;
    public float BaseMeterPerHit => _baseMeterPerHit;
    public float BaseAttackTime => _baseAttackTime;
    public LevelDataCollection LevelData => _levelData;
}
