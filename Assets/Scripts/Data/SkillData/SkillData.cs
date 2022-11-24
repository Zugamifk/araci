using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillData : ScriptableObject
{
    [SerializeField]
    string _key;
    [SerializeField]
    string _displayName;
    [SerializeField]
    string _description;
    [SerializeField]
    Sprite _icon;
    [SerializeField]
    List<SkillData> _requiredSkills = new(); 

    public string Key => _key;
    public string DisplayName => _displayName;
    public string Description => _description;
    public Sprite Icon => _icon;
    public List<SkillData> RequiredSkills => _requiredSkills;   
}
