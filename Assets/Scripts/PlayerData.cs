using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : ScriptableObject
{
    [System.Serializable]
    public class LevelData
    {
        public int NextLevelExperience;
    }

    public int MaxHealth;
    public LevelData[] Levels;
}
