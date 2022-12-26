using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

namespace Food
{
    public class Water : Liquid
    {
        public static Water Warm()
        {
            var water = new Water();
            water.Temperature = Temperature.Warm;
            water.LiquidState = LiquidState.Liquid;
            return water;
        }
    }
}