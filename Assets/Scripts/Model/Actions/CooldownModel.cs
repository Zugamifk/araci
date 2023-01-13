using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CooldownModel : ICooldownModel
{
    /// <summary>
    /// How long the cooldown is
    /// </summary>
    public float Duration { get; set; }

    /// <summary>
    /// When the cooldown is ready
    /// </summary>
    public float ReadyTime { get; set; }
}
