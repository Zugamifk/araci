using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CooldownModel : ICooldownModel
{
    public float Cooldown { get; set; }
    public float ReadyTime { get; set; }
}
