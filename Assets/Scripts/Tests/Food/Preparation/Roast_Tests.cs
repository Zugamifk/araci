using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Food.Tests
{
    public class Roast_Tests
    {
        PreparationService _preparationService = new();
        Roast _roastStep = new();

        [Test]
        public void AffectsAllContainerContents()
        {
            var ingredient1 = new ICookable_Mock();
            ingredient1.Name = "1";
            ingredient1.Moisture = MoistureState.Moist;
            ingredient1.CookState = CookState.Raw;

            var ingredient2 = new ICookable_Mock();
            ingredient2.Name = "2";
            ingredient2.Moisture = MoistureState.Saturated;
            ingredient2.CookState = CookState.Raw;

            var ingredients = new List<Ingredient>() {
                ingredient1,
                ingredient2
            };
            var steps = new List<PreparationStep>()
            {
                new AddToContainer("1"),
                new AddToContainer("2"),
                new Roast()
            };

            _preparationService.Prepare(ingredients, new Container_Mock(), steps);

            Assert.AreEqual(CookState.Cooked, ingredient1.CookState);
            Assert.AreEqual(CookState.Cooked, ingredient2.CookState);
        }

        [Test]
        public void NonCookableIngredient_DoesNotThrow()
        {

            var ingredient = new Ingredient_Mock();
            ingredient.Name = "1";

            var ingredients = new List<Ingredient>() {
                ingredient
            };
            var steps = new List<PreparationStep>()
            {
                new AddToContainer("1")
            };

            var cxt = _preparationService.Prepare(ingredients, new Container_Mock(), steps);

            Assert.DoesNotThrow(() => _roastStep.Do(cxt));
        }
    }
}
