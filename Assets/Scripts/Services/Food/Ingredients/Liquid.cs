using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Food
{
    public abstract class Liquid : Ingredient
    {
        public Temperature Temperature { get; set; }
        public LiquidState LiquidState { get; set; }
    }
}