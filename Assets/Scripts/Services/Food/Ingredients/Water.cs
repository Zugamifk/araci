using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

namespace Food
{
    public class Water : Liquid
    {
        public override string Key => "Water";

        public static Water Warm()
        {
            var water = new Water();
            water.Temperature = Temperature.Warm;
            return water;
        }
    }
}