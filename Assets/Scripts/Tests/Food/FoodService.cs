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

        #region ValidateFood
        [Test]
        public void ValidateFood_ZeroWeight_ThrowsArgumentException()
        {
            var food = new IFood_Mock();
            food.Weight = 0;
            Assert.Throws<ArgumentException>(() => _foodService.ValidateFood(food));
        }

        [Test]
        public void ValidateFood_ZeroVolume_ThrowsArgumentException()
        {
            var food = new IFood_Mock();
            food.Volume = 0;
            Assert.Throws<ArgumentException>(() => _foodService.ValidateFood(food));
        }

        [Test]
        public void ValidateFood_HeatTransferRateNegative_ThrowsArgumentException()
        {
            var food = new IFood_Mock();
            food.HeatTransferRate = -1;
            Assert.Throws<ArgumentException>(() => _foodService.ValidateFood(food));
        }

        [Test]
        public void ValidateFood_SolidPercentNegative_ThrowsArgumentException()
        {
            var food = new IFood_Mock();
            food.SolidPercent = -1;
            Assert.Throws<ArgumentException>(() => _foodService.ValidateFood(food));
        }

        [Test]
        public void ValidateFood_SolidPercentAboveOne_ThrowsArgumentException()
        {
            var food = new IFood_Mock();
            food.SolidPercent = 2;
            Assert.Throws<ArgumentException>(() => _foodService.ValidateFood(food));
        }

        [Test]
        public void ValidateFood_CookedPercentNegative_ThrowsArgumentException()
        {
            var food = new IFood_Mock();
            food.CookedPercent = -1;
            Assert.Throws<ArgumentException>(() => _foodService.ValidateFood(food));
        }

        [Test]
        public void ValidateFood_CookedPercentAboveOne_ThrowsArgumentException()
        {
            var food = new IFood_Mock();
            food.CookedPercent = 2;
            Assert.Throws<ArgumentException>(() => _foodService.ValidateFood(food));
        }

        [Test]
        public void ValidateFood_CookRateNegative_ThrowsArgumentException()
        {
            var food = new IFood_Mock();
            food.CookRate = -1;
            Assert.Throws<ArgumentException>(() => _foodService.ValidateFood(food));
        }
        #endregion

        #region Heat
        [Test]
        [TestCase(1, 1, 1, 100, 100, 100, TestName = "Trivial case")]
        [TestCase(1, 1, 1, 100, 200, 200, TestName = "Trivial case with temp differential")]
        [TestCase(1, 1, 1, 200, 100, 100, TestName = "Trivial case with negative temp differential")]

        [TestCase(2, 1, 1, 100, 200, 150, TestName = "Double Weight")]
        [TestCase(.5f, 1, 1, 100, 200, 200, TestName = "Half Weight")]
        [TestCase(1, 2, 1, 100, 200, 200, TestName = "Double Volume")]
        [TestCase(1, .5f, 1, 100, 200, 150, TestName = "Half Volume")]
        [TestCase(1, 1, 2, 100, 200, 200, TestName = "Double Heat Transfer Rate")]
        [TestCase(1, 1, .5f, 100, 200, 150, TestName = "Half Heat Transfer Rate")]

        [TestCase(2, 1, 1, 200, 100, 150, TestName = "Double Weight with negative temp differential")]
        [TestCase(.5f, 1, 1, 200, 100, 100, TestName = "Half Weight with negative temp differential")]
        [TestCase(1, 2, 1, 200, 100, 100, TestName = "Double Volume with negative temp differential")]
        [TestCase(1, .5f, 1, 200, 100, 150, TestName = "Half Volume with negative temp differential")]
        [TestCase(1, 1, 2, 200, 100, 100, TestName = "Double Heat Transfer Rate with negative temp differential")]
        [TestCase(1, 1, .5f, 200, 100, 150, TestName = "Half Heat Transfer Rate with negative temp differential")]
        public void Heat_TestTemperatureChange(float weight, float volume, float heatTransferRate, float foodTemp, float externalTemp, float expectedTemp)
        {
            var food = new IFood_Mock();
            food.Weight = weight;
            food.Volume = volume;
            food.HeatTransferRate = heatTransferRate;
            food.Temperature = foodTemp;

            _foodService.Heat(food, externalTemp, 1);

            Assert.AreEqual(expectedTemp, food.Temperature);
        }

        [Test]
        [TestCase(100, 100, 200, 1, 0, 0, TestName = "Trivial case")]
        [TestCase(200, 100, 200, 1, 0, .5f, TestName = "Trivial case with temp differential")]
        [TestCase(100, 200, 200, 1, 0, 0, TestName = "Trivial case with negative temp differential")]

        [TestCase(200, 100, 400, 1, 0, .25f, TestName = "Double volume")]
        [TestCase(200, 100, 100, 1, 0, 1, TestName = "Half volume")]
        [TestCase(200, 100, 200, 2, 0, 1, TestName = "Double cook rate")]
        [TestCase(200, 100, 200, .5f, 0, .25f, TestName = "Half cook rate")]
        [TestCase(200, 100, 200, 1, 1, .25f, TestName = "Fully Solid")]
        public void Heat_TestCookPercentChange(float foodTemp, float cookTemp, float volume, float cookRate, float solidPercent, float expectedProgress)
        {
            var food = new IFood_Mock();
            food.Temperature = foodTemp;
            food.CookTemperature = cookTemp;
            food.Volume = volume;
            food.CookRate = cookRate;
            food.SolidPercent = solidPercent;

            _foodService.Heat(food, foodTemp, 1);

            Assert.AreEqual(expectedProgress, food.CookedPercent);
        }
        #endregion
    }
}
