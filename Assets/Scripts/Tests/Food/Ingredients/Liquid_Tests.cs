using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Food.Tests
{
    public class Liquid_Tests
    {
        [Test]
        // heating
        [TestCase(Temperature.Freezing, Temperature.Boiling, Temperature.Cold)]
        [TestCase(Temperature.Cold, Temperature.Boiling, Temperature.Warm)]
        [TestCase(Temperature.Warm, Temperature.Boiling, Temperature.Hot)]
        [TestCase(Temperature.Hot, Temperature.Boiling, Temperature.Boiling)]
        [TestCase(Temperature.Boiling, Temperature.Boiling, Temperature.Boiling)]

        // cooling
        [TestCase(Temperature.Freezing, Temperature.Freezing, Temperature.Freezing)]
        [TestCase(Temperature.Cold, Temperature.Freezing, Temperature.Freezing)]
        [TestCase(Temperature.Warm, Temperature.Freezing, Temperature.Cold)]
        [TestCase(Temperature.Hot, Temperature.Freezing, Temperature.Warm)]
        [TestCase(Temperature.Boiling, Temperature.Freezing, Temperature.Hot)]

        // equalizing
        [TestCase(Temperature.Freezing, Temperature.Freezing, Temperature.Freezing)]
        [TestCase(Temperature.Cold, Temperature.Cold, Temperature.Cold)]
        [TestCase(Temperature.Warm, Temperature.Warm, Temperature.Warm)]
        [TestCase(Temperature.Hot, Temperature.Hot, Temperature.Hot)]
        [TestCase(Temperature.Boiling, Temperature.Boiling, Temperature.Boiling)]

        public void ApplyHeat_ExpectedTemperatureChange(Temperature start, Temperature applied, Temperature expected)
        {
            var liquid = new Liquid_Mock();
            liquid.Temperature = start;

            liquid.ApplyTemperature(applied);

            Assert.AreEqual(expected, liquid.Temperature);
        }

        [Test]
        [TestCase(Temperature.Freezing, LiquidState.Frozen)]
        [TestCase(Temperature.Cold, LiquidState.Liquid)]
        [TestCase(Temperature.Warm, LiquidState.Liquid)]
        [TestCase(Temperature.Hot, LiquidState.Liquid)]
        [TestCase(Temperature.Boiling, LiquidState.Boiling)]
        public void GetLiquidState_ReturnsExpectedValueByTemperature(Temperature temperature, LiquidState expectedState)
        {
            var liquid = new Liquid_Mock();
            liquid.Temperature = temperature;

            var liquidState = liquid.GetLiquidState();
            Assert.AreEqual(expectedState, liquidState);
        }
    }
}
