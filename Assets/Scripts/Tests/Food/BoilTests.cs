using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using UnityEngine.WSA;
using System;

namespace Food.Tests
{
    public class BoilTests : MonoBehaviour
    {
        Boil _boil = new();

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
            cookable.Moisture = moisture;
            cookable.CookState = cookState;

            _boil.Cook(cookable);

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
            cookable.Moisture = moisture;
            cookable.CookState = cookState;

            _boil.Cook(cookable);

            Assert.AreEqual(expectedMoisture, cookable.Moisture);
        }
    }
}
