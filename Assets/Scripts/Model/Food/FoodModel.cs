using Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Food
{
    public struct FoodModel : IFoodModel
    {
        public string Name { get; }

        public float Amount { get; }

        public bool IsRaw { get; set; }

        public FoodModel(string name, float amount)
        {
            Name = name;
            Amount = amount;
            IsRaw = true;
        }
    }
}