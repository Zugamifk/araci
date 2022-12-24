using Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Food
{
    public class FoodService
    {
        public void Eat(IFoodModel food, float amount)
        {
            throw new NotImplementedException();
        }

        public IFoodModel GetFood(string name, float measure)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Name can not be empty!");
            } else if (measure <= 0)
            {
                throw new ArgumentException("Measure must be greater than 0!");
            }

            var food = new FoodModel();
            food.Name = name;
            food.Amount = measure;
            return food;
        }
    }
}