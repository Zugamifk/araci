using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Food.Tests
{
    public class IFood_Mock : IFood
    {
        public string Name { get; set; } = "Mock Food";
        public float Weight { get; set; } = 1000;
        public float Volume { get; set; } = 1000;
    }
}
