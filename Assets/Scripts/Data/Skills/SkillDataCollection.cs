using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDataCollection : ScriptableObject
{
    [SerializeField]
    SkillData[] _allSkills;

    private void OnEnable()
    {
        
    }
}
