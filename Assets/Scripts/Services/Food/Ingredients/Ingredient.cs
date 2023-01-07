using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Food
{
    public abstract class Ingredient
    {
        public abstract string Key { get; }

        public float Volume;
    }
}