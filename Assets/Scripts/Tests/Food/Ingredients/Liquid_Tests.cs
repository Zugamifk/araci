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
        [TestCase(Temperature.Freezing, Temperature.Scalding, Temperature.Cold)]
        [TestCase(Temperature.Cold, Temperature.Scalding, Temperature.Warm)]
        [TestCase(Temperature.Warm, Temperature.Scalding, Temperature.Hot)]
        [TestCase(Temperature.Hot, Temperature.Scalding, Temperature.Scalding)]
        [TestCase(Temperature.Scalding, Temperature.Scalding, Temperature.Scalding)]

        // cooling
        [TestCase(Temperature.Freezing, Temperature.Freezing, Temperature.Freezing)]
        [TestCase(Temperature.Cold, Temperature.Freezing, Temperature.Freezing)]
        [TestCase(Temperature.Warm, Temperature.Freezing, Temperature.Cold)]
        [TestCase(Temperature.Hot, Temperature.Freezing, Temperature.Warm)]
        [TestCase(Temperature.Scalding, Temperature.Freezing, Temperature.Hot)]

        // equalizing
        [TestCase(Temperature.Freezing, Temperature.Freezing, Temperature.Freezing)]
        [TestCase(Temperature.Cold, Temperature.Cold, Temperature.Cold)]
        [TestCase(Temperature.Warm, Temperature.Warm, Temperature.Warm)]
        [TestCase(Temperature.Hot, Temperature.Hot, Temperature.Hot)]
        [TestCase(Temperature.Scalding, Temperature.Scalding, Temperature.Scalding)]

        public void ApplyHeat_ExpectedTemperatureChange(Temperature start, Temperature applied, Temperature expected)
        {
            var liquid = new Liquid_Mock();
            liquid.Temperature = start;

            liquid.ApplyTemperature(applied);

            Assert.AreEqual(expected, liquid.Temperature);
        }
    }
}
