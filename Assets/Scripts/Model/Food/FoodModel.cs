using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Food
{
    public class FoodModel : IFoodModel, IFood
    {
        public string Name { get; set; }
        public float Temperature { get; set; }
        public float CookTemperature { get; set; } 
        public float Weight { get; set; }
        public float Volume { get; set; }
        public float HeatTransferRate { get; set; }
        public float SolidPercent { get; set; }
        public float CookedPercent { get; set; }
        public float CookRate { get; set; }
    }
}