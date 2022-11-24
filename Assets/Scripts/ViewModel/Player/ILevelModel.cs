using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILevelModel
{
    int CurrentLevel { get; }
    int CurrentExperience { get; }
    int LastLevelRequiredExperience { get; }
    int RequiredExperience { get; }
}
