using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Food
{
    public class PreparationContext
    {
        public Description Description = new();
        public Container Container;
        public Dictionary<string, Ingredient> Ingredients = new();
    }
}