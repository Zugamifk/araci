using Food;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Food
{
    public interface ILiquid
    {
        float Temperature { get; set; }
        float FreezeTemperature { get; }
        float BoilTemperature { get; }
    }
}