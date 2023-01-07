using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Food
{
    public abstract class Recipe
    {
        protected PreparationService preparationService = new();

        public abstract string Name { get; }

        public abstract PreparationContext Prepare();
    }
}