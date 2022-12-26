using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;

namespace Food.Tests
{
    public class RoastProcess_Tests
    {
        RoastProcess _roast = new();

        [Test]
        [TestCase(MoistureState.Dry, CookState.Raw, CookState.Burnt)]
        [TestCase(MoistureState.Dry, CookState.Cooked, CookState.Burnt)]
        [TestCase(MoistureState.Dry, CookState.Burnt, CookState.Burnt)]
        [TestCase(MoistureState.Moist, CookState.Raw, CookState.Cooked)]
        [TestCase(MoistureState.Moist, CookState.Cooked, CookState.Cooked)]
        [TestCase(MoistureState.Moist, CookState.Burnt, CookState.Burnt)]
        [TestCase(MoistureState.Saturated, CookState.Raw, CookState.Cooked)]
        [TestCase(MoistureState.Saturated, CookState.Cooked, CookState.Cooked)]
        [TestCase(MoistureState.Saturated, CookState.Burnt, CookState.Burnt)]
        public void Cook_ExpectedCookState(MoistureState moisture, CookState cookState, CookState expectedCookState)
        {
            var cookable = new ICookable_Mock();
            cookable.Moisture = moisture;
            cookable.CookState = cookState;

            _roast.Process(cookable);

            Assert.AreEqual(expectedCookState, cookable.CookState);
        }

        [Test]
        [TestCase(MoistureState.Dry, CookState.Raw, MoistureState.Dry)]
        [TestCase(MoistureState.Dry, CookState.Cooked, MoistureState.Dry)]
        [TestCase(MoistureState.Dry, CookState.Burnt, MoistureState.Dry)]
        [TestCase(MoistureState.Moist, CookState.Raw, MoistureState.Moist)]
        [TestCase(MoistureState.Moist, CookState.Cooked, MoistureState.Dry)]
        [TestCase(MoistureState.Moist, CookState.Burnt, MoistureState.Dry)]
        [TestCase(MoistureState.Saturated, CookState.Raw, MoistureState.Saturated)]
        [TestCase(MoistureState.Saturated, CookState.Cooked, MoistureState.Moist)]
        [TestCase(MoistureState.Saturated, CookState.Burnt, MoistureState.Moist)]
        public void Cook_ExpectedMoisture(MoistureState moisture, CookState cookState, MoistureState expectedMoisture)
        {
            var cookable = new ICookable_Mock();
            cookable.Moisture = moisture;
            cookable.CookState = cookState;

            _roast.Process(cookable);

            Assert.AreEqual(expectedMoisture, cookable.Moisture);
        }
    }
}
