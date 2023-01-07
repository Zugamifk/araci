using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace Food
{
    public class AddToContainer : PreparationStep
    {
        string _ingredientName;

        public AddToContainer(string ingredientName)
        {
            _ingredientName = ingredientName;
        }

        public override void Do(PreparationContext context)
        {
            var ingredient = context.Ingredients[_ingredientName];
            if (ingredient.Volume + context.Container.GetContentsVolume() > context.Container.Volume)
            {
                throw new InvalidOperationException($"Not enough space in container to add ingredient!");
            }

            context.Container.Contents.Add(ingredient);

            context.Description.MainIngredient = ingredient.Key;
        }
    }
}