using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthModel : IHealthModel
{
    public int CurrentHealth { get; set; }
    public int MaxHealth { get; set; }
    public bool IsAlive => CurrentHealth > 0;
}
