using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Flags]
public enum TargetType
{
    None = 0,
    Player = 1,
    Enemy = 2,
    All = Player | Enemy,
}
