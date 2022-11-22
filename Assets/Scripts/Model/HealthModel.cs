using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthModel : IHealthModel
{
    public Guid Id { get; }
    public int HitPoints { get; set; }
}
