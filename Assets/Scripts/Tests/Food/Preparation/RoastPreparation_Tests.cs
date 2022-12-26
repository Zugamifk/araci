using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Food.Tests
{
    public class RoastPreparation_Tests
    {
        PreparationService _preparationService = new();
        RoastPreparation _roastStep = new();

        [Test]
        public void AffectsAllContainerContents()
        {
            var cxt = new PreparationContext();
            cxt.Container = new Pot();

            var ingredient1 = new ICookable_Mock();
            ingredient1.Moisture = MoistureState.Moist;
            ingredient1.CookState = CookState.Raw;

            var ingredient2 = new ICookable_Mock();
            ingredient2.Moisture = MoistureState.Saturated;
            ingredient2.CookState = CookState.Raw;
            _preparationService.AddToContainer(ingredient1, cxt.Container);
            _preparationService.AddToContainer(ingredient2, cxt.Container);

            _roastStep.Do(cxt);

            Assert.AreEqual(CookState.Cooked, ingredient1.CookState);
            Assert.AreEqual(CookState.Cooked, ingredient2.CookState);
        }

        [Test]
        public void NonCookableIngredient_DoesNotThrow()
        {
            var cxt = new PreparationContext();
            cxt.Container = new Pot();

            var ingredient1 = new Ingredient_Mock();
            _preparationService.AddToContainer(ingredient1, cxt.Container);

            Assert.DoesNotThrow(() => _roastStep.Do(cxt));
        }
    }
}
