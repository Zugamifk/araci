using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealthModel
{
    int CurrentHealth { get; }
    int MaxHealth { get; }

}
