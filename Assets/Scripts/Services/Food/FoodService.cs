using Codice.Client.Common;
using Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Food
{
    public class FoodService : IFoodService
    {
        public void Cook(ICookable cookable, ICookingMethod method)
        {
            method.Cook(cookable);
        }

        public void AddToContainer(Ingredient ingredient, Container container)
        {
            if(ingredient.Volume + container.GetContentsVolume() > container.Volume)
            {
                throw new InvalidOperationException($"Not enough space in container to add ingredient!");
            }

            container.Contents.Add(ingredient);
        }
    }
}