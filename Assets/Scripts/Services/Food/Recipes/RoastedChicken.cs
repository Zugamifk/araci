using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Food
{
    public class RoastedChicken : Recipe
    {
        public override string Name => "Roasted Chicken";

        public override PreparationContext Prepare()
        {
            var chicken = new Chicken();

            var ingredients = new List<Ingredient>() {
                chicken
            };
            var steps = new List<PreparationStep>()
            {
                new AddToContainer(chicken.Key),
                new Roast()
            };

            return preparationService.Prepare(ingredients, new Cauldron(), steps);
        }
    }
}