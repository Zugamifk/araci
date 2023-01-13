using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDataCollection : ScriptableObject, IRegisteredData
{
    [SerializeField]
    LevelData[] _levelData;
    
    public LevelData GetLevel(int level) => _levelData[level];
}
