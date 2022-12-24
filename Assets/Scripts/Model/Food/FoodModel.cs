using Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Food
{
    public class FoodModel : IFoodModel
    {
        public string Name { get; set; }

        public float Amount { get; set; }
    }
}