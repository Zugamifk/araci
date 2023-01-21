using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDashModel
{
    ICooldownModel Cooldown { get; }
    float Speed { get; }
    float Duration { get; }
}
