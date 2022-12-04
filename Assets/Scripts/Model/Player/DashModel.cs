using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashModel : IDashModel
{
    public float Duration { get; set; }
    public float Speed { get; set; }
    public CooldownModel Cooldown { get; } = new();

    ICooldownModel IDashModel.Cooldown => Cooldown;
}
