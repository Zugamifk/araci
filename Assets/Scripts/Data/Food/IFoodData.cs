using Mono.CompilerServices.SymbolWriter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Food
{
    public interface IFoodData
    {
        string Name { get; set; }
        float Weight { get; set; }
        float Volume { get; set; }
        float HeatTransferRate { get; set; }
        float SolidPercent { get; set; }
        float CookTemperature { get; set; }
        float CookRate { get; set; }
    }
}