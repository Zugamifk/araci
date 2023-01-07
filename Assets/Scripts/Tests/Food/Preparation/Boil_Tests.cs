using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Food.Tests
{
    public class Boil_Tests
    {
        PreparationService _preparationService = new();

        [Test]
        public void EmptyContainer_ThrowsInvalidOperationException()
        {
            var steps = new List<PreparationStep>()
            {
                new Boil()
            };

            Assert.Throws<InvalidOperationException>(() => _preparationService.Prepare(new(), new Container_Mock(), steps));
        }

        [Test]
        public void NoLiquid_ThrowsInvalidOperationException()
        {
            var ingredient = new ICookable_Mock();
            ingredient.Name = "1";
            ingredient.Moisture = MoistureState.Saturated;
            ingredient.CookState = CookState.Raw;

            var ingredients = new List<Ingredient>() {
                ingredient
            };

            var steps = new List<PreparationStep>()
            {
                new AddToContainer("1"),
                new Boil()
            };

            Assert.Throws<InvalidOperationException>(() => _preparationService.Prepare(ingredients, new Container_Mock(), steps));
        }

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
                new Boil()
            };

            _preparationService.Prepare(ingredients, new Container_Mock(), steps);

            Assert.AreEqual(CookState.Cooked, ingredient1.CookState);
            Assert.AreEqual(CookState.Cooked, ingredient2.CookState);
        }

        [Test]
        [TestCase(MoistureState.Dry, CookState.Raw, CookState.Raw)]
        [TestCase(MoistureState.Dry, CookState.Cooked, CookState.Cooked)]
        [TestCase(MoistureState.Dry, CookState.Burnt, CookState.Burnt)]
        [TestCase(MoistureState.Moist, CookState.Raw, CookState.Raw)]
        [TestCase(MoistureState.Moist, CookState.Cooked, CookState.Cooked)]
        [TestCase(MoistureState.Moist, CookState.Burnt, CookState.Burnt)]
        [TestCase(MoistureState.Saturated, CookState.Raw, CookState.Cooked)]
        [TestCase(MoistureState.Saturated, CookState.Cooked, CookState.Cooked)]
        [TestCase(MoistureState.Saturated, CookState.Burnt, CookState.Burnt)]
        public void Cook_ExpectedCookState(MoistureState moisture, CookState cookState, CookState expectedCookState)
        {
            var cookable = new ICookable_Mock();
            cookable.Name = "1";
            cookable.Moisture = moisture;
            cookable.CookState = cookState;

            var ingredients = new List<Ingredient>() {
                cookable
            };

            var steps = new List<PreparationStep>()
            {
                new AddToContainer("1"),
                new Boil()
            };

            _preparationService.Prepare(ingredients, new Container_Mock(), steps);

            Assert.AreEqual(expectedCookState, cookable.CookState);
        }

        [Test]
        [TestCase(MoistureState.Dry, CookState.Raw, MoistureState.Moist)]
        [TestCase(MoistureState.Dry, CookState.Cooked, MoistureState.Moist)]
        [TestCase(MoistureState.Dry, CookState.Burnt, MoistureState.Moist)]
        [TestCase(MoistureState.Moist, CookState.Raw, MoistureState.Saturated)]
        [TestCase(MoistureState.Moist, CookState.Cooked, MoistureState.Saturated)]
        [TestCase(MoistureState.Moist, CookState.Burnt, MoistureState.Saturated)]
        [TestCase(MoistureState.Saturated, CookState.Raw, MoistureState.Saturated)]
        [TestCase(MoistureState.Saturated, CookState.Cooked, MoistureState.Saturated)]
        [TestCase(MoistureState.Saturated, CookState.Burnt, MoistureState.Saturated)]
        public void Cook_ExpectedMoisture(MoistureState moisture, CookState cookState, MoistureState expectedMoisture)
        {
            var cookable = new ICookable_Mock();
            cookable.Name = "1";
            cookable.Moisture = moisture;
            cookable.CookState = cookState;

            var ingredients = new List<Ingredient>() {
                cookable
            };

            var steps = new List<PreparationStep>()
            {
                new AddToContainer("1"),
                new Boil()
            };

            _preparationService.Prepare(ingredients, new Container_Mock(), steps);

            Assert.AreEqual(expectedMoisture, cookable.Moisture);
        }
    }
}
