using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Food
{
    public abstract class Cookable : Ingredient
    {
        public CookState CookState { get; set; }
        public MoistureState Moisture { get; set; }
    }
}