using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Food;
using System;

namespace Food.Tests
{
    public class AddToContainer_Tests
    {
        PreparationService _preparationService = new();

        #region AddToContainer
        [Test]
        public void AddToContainer_AddsToContainerVolume()
        {
            var ingredient = new Ingredient_Mock();
            ingredient.MockKey = "1";
            ingredient.Volume = 250;

            var ingredients = new List<Ingredient>() { 
                ingredient 
            };
            var steps = new List<PreparationStep>() {
                new AddToContainer("1"),
            };

            var cxt = _preparationService.Prepare(ingredients, new Container_Mock(), steps);

            Assert.AreEqual(ingredient.Volume, cxt.Container.GetContentsVolume());
        }

        [Test]
        public void AddToContainer_TwoItems_AddsToContainerVolume()
        {
            var ingredient1 = new Ingredient_Mock();
            ingredient1.MockKey = "1";
            ingredient1.Volume = 250;
            var ingredient2 = new Ingredient_Mock();
            ingredient2.MockKey = "2";
            ingredient2.Volume = 450;

            var ingredients = new List<Ingredient>() { 
                ingredient1, 
                ingredient2 
            };
            var steps = new List<PreparationStep>() {
                new AddToContainer("1"),
                new AddToContainer("2")
            };

            var cxt = _preparationService.Prepare(ingredients, new Container_Mock(), steps);

            float sumVolume = ingredient1.Volume + ingredient2.Volume;
            Assert.AreEqual(sumVolume, cxt.Container.GetContentsVolume());
        }

        [Test]
        public void AddToContainer_NoSpace_ThrowsInvalidOperationException()
        {
            var ingredient = new Ingredient_Mock();
            ingredient.MockKey = "1";
            ingredient.Volume = 1200;

            var ingredients = new List<Ingredient>() {
                ingredient
            };
            var steps = new List<PreparationStep>() {
                new AddToContainer("1")
            };

            Assert.Throws<InvalidOperationException>(() => _preparationService.Prepare(ingredients, new Container_Mock(1000), steps));
        }
        #endregion
    }
}
