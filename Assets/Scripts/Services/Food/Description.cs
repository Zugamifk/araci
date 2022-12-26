using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Food {
    public class Description
    {
        public string MainIngredient;
        public string SpecialDishName;
        public string CookingMethod;

        public override string ToString()
        {
            string name = CookingMethod;
            if(!string.IsNullOrEmpty(MainIngredient))
            {
                name += " " + MainIngredient;
            }

            if(!string.IsNullOrEmpty(SpecialDishName))
            {
                name += " " + SpecialDishName;
            }
            return name;
        }
    }
}