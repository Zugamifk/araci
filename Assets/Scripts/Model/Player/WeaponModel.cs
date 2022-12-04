using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponModel : IWeaponModel
{
    public string Key { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public LevelModel Level { get; } = new();
}
