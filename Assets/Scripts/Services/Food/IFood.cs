using Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Food
{
    public interface IFood : IIngredient, IHeatable
    {
        float CookTemperature { get; set; }
        float CookedPercent { get; set; }
        float CookRate { get; set; }
    }
}
