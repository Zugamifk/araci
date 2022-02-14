using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressionData : ScriptableObject
{
    [System.Serializable]
    public class LevelData
    {
        public int NextLevelExperience;
    }

    public LevelData[] Levels;
}
