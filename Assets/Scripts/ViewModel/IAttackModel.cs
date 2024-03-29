using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttackModel
{
    float Range { get; }
    ICooldownModel Cooldown { get; }
}
