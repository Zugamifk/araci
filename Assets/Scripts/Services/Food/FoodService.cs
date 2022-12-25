using Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Food
{
    public class FoodService
    {
        public void Eat(IFood food, float amount)
        {
            if (amount > food.Amount)
            {
                throw new InvalidOperationException($"Can not eat {amount!} Only {food.Amount} left.");
            }
            else if (amount < 0)
            {
                throw new ArgumentException($"Can not eat {amount}! Amount to eat can not be negative!");
            }

            food.Amount -= amount;
        }

        public void Heat(IFood food, float temperatureChange)
        {
            food.Temperature += temperatureChange;
        }
    }
}