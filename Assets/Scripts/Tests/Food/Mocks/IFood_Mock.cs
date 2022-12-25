using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Food.Tests
{
    public class IFood_Mock : IFood
    {
        public string Name { get; set; } = "Mock Food";
        public float Amount { get; set; } = 1000;
        public float Temperature { get; set; } = 50;
        public float CookTemperature { get; set; } = 100;
        public float Weight { get; set; } = 1000;
        public float Volume { get; set; } = 1000;
        public float HeatTransferRate { get; set; } = .5f;
        public float SolidPercent { get; set; } = .5f;
        public float CookedPercent { get; set; } = 0;
        public float CookRate { get; set; } = .5f;
    }
}
