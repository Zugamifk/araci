using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealthModel : IIdentifiable
{
    public int HitPoints { get; }
}
