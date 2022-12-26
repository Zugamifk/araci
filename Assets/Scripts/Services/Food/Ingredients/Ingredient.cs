using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Food
{
    public abstract class Ingredient
    {
        public string Name;

        public float Volume;

        public MoistureState Moisture { get; set; }
    }
}