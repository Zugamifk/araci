using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Food.Tests
{
    public class Meat_Mock : Meat
    {
        public Meat_Mock()
        {
            Name = "Meat Mock";
            CookState = CookState.Raw;
            Volume = 250;
            Moisture = MoistureState.Dry;
        }
    }
}
