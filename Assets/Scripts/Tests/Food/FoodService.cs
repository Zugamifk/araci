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
        const string TEST_FOOD_NAME = "Test Food";
        const float TEST_FOOD_AMOUNT = 100;
        const float TEST_EAT_AMOUNT = 25;
        const float TEST_FOOD_TEMP = 70;

        FoodService _foodService;

        [OneTimeSetUp]
        public void InstantiateFoodService()
        {
            _foodService = new FoodService();
        }

        #region EatFood
        [Test]
        public void EatFood_RemovesAmount()
        {
            var food = new IFood_Mock(TEST_FOOD_NAME, TEST_FOOD_AMOUNT, TEST_FOOD_TEMP);

            _foodService.Eat(food, TEST_EAT_AMOUNT);

            float expectedRemaining = TEST_FOOD_AMOUNT - TEST_EAT_AMOUNT;
            Assert.AreEqual(expectedRemaining, food.Amount);
        }

        [Test]
        public void EatFood_EatAmountMoreThanAmount_ThrowsInvalidOperationException()
        {
            var food = new IFood_Mock(TEST_FOOD_NAME, TEST_FOOD_AMOUNT, TEST_FOOD_TEMP);

            Assert.Throws<InvalidOperationException>(() => _foodService.Eat(food, TEST_FOOD_AMOUNT + TEST_EAT_AMOUNT));
        }

        [Test]
        public void EatFood_EatAmountNegative_ThrowsArgumentException()
        {
            var food = new IFood_Mock(TEST_FOOD_NAME, TEST_FOOD_AMOUNT, TEST_FOOD_TEMP);

            Assert.Throws<ArgumentException>(() => _foodService.Eat(food, -1));
        }
        #endregion

        #region Heat
        [Test]
        public void Heat_AppliesChange()
        {
            var food = new IFood_Mock(TEST_FOOD_NAME, TEST_EAT_AMOUNT, TEST_FOOD_TEMP);

            float heatAmount = 25;
            _foodService.Heat(food, heatAmount);

            Assert.AreEqual(TEST_FOOD_TEMP + heatAmount, food.Temperature);
        }

        [Test]
        public void Heat_ILiquid_OverEvaporateTemp_IsBoiling()
        {
            var liquid = new ILiquid_Mock();
            liquid.Temperature = liquid.BoilTemperature - 10;

            _foodService.Heat(liquid, 25);

            var state = liquid.GetState();
            Assert.AreEqual(LiquidState.Boiling, state);
        }


        [Test]
        public void Heat_ILiquid_UnderFreezeTemp_IsFrozen()
        {
            var liquid = new ILiquid_Mock();
            liquid.Temperature = liquid.FreezeTemperature + 10;

            _foodService.Heat(liquid, -25);

            var state = liquid.GetState();
            Assert.AreEqual(LiquidState.Frozen, state);
        }

        [Test]
        public void Heat_ILiquid_BetweenFreezeAndBoildTemp_IsLiquid()
        {
            var liquid = new ILiquid_Mock();
            liquid.Temperature = Mathf.Lerp(liquid.FreezeTemperature, liquid.BoilTemperature, .5f);

            var state = liquid.GetState();
            Assert.AreEqual(LiquidState.Liquid, state);
        }

        #endregion
    }
}
