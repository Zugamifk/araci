using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelModel : ILevelModel
{
    public int CurrentLevel { get; set; }
    public int CurrentExperience { get; set; }
    public int RequiredExperience { get; set; }
}
