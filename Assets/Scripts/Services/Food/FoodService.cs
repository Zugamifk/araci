using Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Food
{
    public class FoodService
    {
        public IFoodModel Cook(IFoodModel food)
        {
            var newFood = new FoodModel(food.Name, food.Amount);
            newFood.IsRaw = false;
            return newFood;
        }

        public IFoodModel Eat(IFoodModel food, float amount)
        {
            if (amount > food.Amount)
            {
                throw new InvalidOperationException($"Can not eat {amount!} Only {food.Amount} left.");
            }
            else if (amount < 0)
            {
                throw new ArgumentException($"Can not eat {amount}! Amount to eat can not be negative!");
            }

            return GetFood(food.Name, food.Amount - amount);
        }

        public IFoodModel GetFood(string name, float measure)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Name can not be empty!");
            }
            else if (measure <= 0)
            {
                throw new ArgumentException("Measure must be greater than 0!");
            }

            var food = new FoodModel(name, measure);
            return food;
        }
    }
}