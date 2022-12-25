using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Food
{
    public static class IFoodExtension
    {
        public static bool IsCooked(this IFood food)
        {
            return food.Temperature > food.CookTemperature;
        }
    }
}