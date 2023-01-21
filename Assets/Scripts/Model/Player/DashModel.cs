using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashModel : IDashModel
{
    public CooldownModel Cooldown { get; } = new();
    public float Speed { get; set; }
    public float Duration { get; set; }

    ICooldownModel IDashModel.Cooldown => Cooldown;
}
