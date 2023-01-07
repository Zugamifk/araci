using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Food.Tests
{
    public class Liquid_Mock : Liquid
    {
        public string MockKey;
        public override string Key => MockKey;
    }
}
