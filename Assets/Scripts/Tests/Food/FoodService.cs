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
        [TestCase(1, 1, 1, 100, 100, 100)]
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
        #endregion
    }
}
