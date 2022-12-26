using Codice.Client.Common;
using Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Food
{
    public class PreparationService : IFoodService
    {
        public PreparationContext Prepare(List<Ingredient> ingredients, Container container, List<PreparationStep> steps)
        {
            var preparation = new Preparation();
            var context = new PreparationContext();
            foreach (var i in ingredients)
            {
                context.Ingredients[i.Name] = i;
            }
            context.Container = container;
            preparation.Context = context;

            preparation.Steps = steps;

            foreach (var step in steps)
            {
                step.Do(context);
            }

            return context;
        }
    }
}