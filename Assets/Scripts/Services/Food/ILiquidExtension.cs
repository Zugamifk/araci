using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Food
{
    public static class ILiquidExtension
    {
        public static LiquidState GetState(this ILiquid liquid) => liquid.Temperature switch
        {
            var t when t <= liquid.FreezeTemperature => LiquidState.Frozen,
            var t when t >= liquid.BoilTemperature => LiquidState.Boiling,
            _ => LiquidState.Liquid
        };
    }
}