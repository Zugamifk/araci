using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Food.Tests
{
    public class Meat_Mock : Meat
    {
        public string MockKey;
        public override string Key => MockKey;

        public Meat_Mock()
        {
            CookState = CookState.Raw;
            Volume = 250;
            Moisture = MoistureState.Dry;
        }
    }
}
