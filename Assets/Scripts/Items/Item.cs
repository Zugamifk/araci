using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Item")]
public class Item : ScriptableObject
{
    [System.Serializable]
    public class LevelInfo {
        public string Description;
    }

    public string Name;
    public Sprite Icon;
    public LevelInfo[] Levels;

    public int MaxLevel => Levels.Length;
    
    public virtual ItemState GetNewState()
    {
        return new ItemState()
        {
            Level = 0
        };
    }
}
