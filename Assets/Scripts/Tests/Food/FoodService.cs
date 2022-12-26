using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Food;
using System;

namespace Food.Tests
{
    public class FoodServiceTests
    {
        FoodService _foodService = new();

        #region Cook
        [Test]
        public void Cook_Roast_Raw_IsCooked()
        {
            var cookable = new ICookable_Mock();
            cookable.CookState = CookState.Raw;

            _foodService.Cook(cookable, new Roast());

            Assert.AreEqual(CookState.Cooked, cookable.CookState);
        }

        [Test]
        public void Cook_Roast_Cooked_IsBurnt()
        {
            var cookable = new ICookable_Mock();
            cookable.CookState = CookState.Cooked;

            _foodService.Cook(cookable, new Roast());

            Assert.AreEqual(CookState.Burnt, cookable.CookState);
        }

        [Test]
        public void Cook_Roast_Burnt_IsBurnt()
        {
            var cookable = new ICookable_Mock();
            cookable.CookState = CookState.Burnt;

            _foodService.Cook(cookable, new Roast());

            Assert.AreEqual(CookState.Burnt, cookable.CookState);
        }
        #endregion

        #region AddToContainer
        [Test]
        public void AddToContainer_AddsToContainerVolume()
        {
            var ingredient = new Ingredient_Mock();
            ingredient.Volume = 250;
            var container = new IContainer_Mock(1000);

            _foodService.AddToContainer(ingredient, container);

            Assert.AreEqual(ingredient.Volume, container.GetContentsVolume());
        }

        [Test]
        public void AddToContainer_TwoItems_AddsToContainerVolume()
        {
            var ingredient1 = new Ingredient_Mock();
            ingredient1.Volume = 250;
            var ingredient2 = new Ingredient_Mock();
            ingredient2.Volume = 450;
            var container = new IContainer_Mock(1000);

            _foodService.AddToContainer(ingredient1, container);
            _foodService.AddToContainer(ingredient2, container);

            float sumVolume = ingredient1.Volume + ingredient2.Volume;
            Assert.AreEqual(sumVolume, container.GetContentsVolume());
        }

        [Test]
        public void AddToContainer_NoSpace_ThrowsInvalidOperationException()
        {
            var ingredient = new Ingredient_Mock();
            ingredient.Volume = 1200;
            var container = new IContainer_Mock(1000);

            Assert.Throws<InvalidOperationException>(()=>_foodService.AddToContainer(ingredient, container));
        }
        #endregion
    }
}
