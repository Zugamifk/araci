using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICooldownModel
{
    float Duration
    {
        get;
    }
    float ReadyTime { get; }
}
