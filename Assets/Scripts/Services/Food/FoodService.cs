using Codice.Client.Common;
using Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Food
{
    public class FoodService
    {
        public void ValidateFood(IFood food)
        {
            if (food.Weight <= 0)
            {
                throw new ArgumentException($"Food has weight of zero and therefore has nothing left!");
            }

            if (food.Volume <= 0)
            {
                throw new ArgumentException($"Food has volume of zero and therefore has nothing left!");
            }

            if (food.HeatTransferRate < 0)
            {
                throw new ArgumentException($"Heat transfer rate is {food.HeatTransferRate}! Can not be negative.");
            }

            if (food.SolidPercent < 0 || food.SolidPercent > 1)
            {
                throw new ArgumentException($"Heat SolidPercent is {food.SolidPercent}! Must be between 0 and 1.");
            }

            if (food.CookedPercent < 0 || food.CookedPercent > 1)
            {
                throw new ArgumentException($"Heat SolidPercent is {food.SolidPercent}! Must be between 0 and 1.");
            }

            if (food.CookRate < 0)
            {
                throw new ArgumentException($"Cook rate is {food.CookRate}! Can not be negative.");
            }
        }

        public void Heat(IFood food, float externalTemperature, float time)
        {
            ValidateFood(food);

            UpdateTemperature(food, externalTemperature, time);

            if (food.Temperature >= food.CookTemperature)
            {
                UpdateCookedPercent(food, time);
            }
        }

        public void Heat(ILiquid liquid, float externalTemperature, float time)
        {
            UpdateTemperature(liquid, externalTemperature, time);
        }

        void UpdateTemperature(IHeatable heatable, float externalTemperature, float time)
        {
            var density = heatable.Weight / heatable.Volume;
            var heatConductivity = (float)heatable.HeatTransferRate / density;
            var heatDifferential = (externalTemperature - heatable.Temperature);

            heatable.Temperature += time * heatDifferential * heatConductivity;

            // its possible that things can heat up in less than the given time, so clamp it
            if(heatDifferential > 0)
            {
                heatable.Temperature = Mathf.Min(heatable.Temperature, externalTemperature);
            } else
            {
                heatable.Temperature = Mathf.Max(heatable.Temperature, externalTemperature);
            }
        }

        void UpdateCookedPercent(IFood food, float time)
        {
            var tempDifferential = food.Temperature - food.CookTemperature;
            var cookedAmount = food.Volume * food.CookedPercent;
            cookedAmount += time * tempDifferential * food.CookRate / (1 + food.SolidPercent);
            food.CookedPercent = Mathf.Clamp01(cookedAmount / food.Volume);
        }
    }
}