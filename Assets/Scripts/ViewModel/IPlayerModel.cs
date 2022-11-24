using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerModel : IIdentifiable
{
    IWeaponModel Weapon { get; }
    ILevelModel Level { get; }
}
