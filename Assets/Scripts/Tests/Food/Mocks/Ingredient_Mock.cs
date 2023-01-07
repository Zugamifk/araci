using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Food.Tests
{
    public class Ingredient_Mock : Ingredient
    {
        public string MockKey;
        public override string Key => MockKey;

        public Ingredient_Mock()
        {
            Volume = 250;
        }   
    }
}
