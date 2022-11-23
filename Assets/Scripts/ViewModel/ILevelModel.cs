using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILevelModel
{
    int CurrentLevel { get; }
    int CurrentExperience { get; }
    int RequiredExperience { get; }
}
