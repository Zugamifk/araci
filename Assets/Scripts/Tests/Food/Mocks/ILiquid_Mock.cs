using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Food.Tests
{
    public class ILiquid_Mock : ILiquid, IFood
    {
        public float Temperature { get; set; }

        public float FreezeTemperature => 0;

        public float BoilTemperature => 100;

        public string Name { get; set; } = "Liquid Mock";
        public float Amount { get; set; } = 1;
    }
}
