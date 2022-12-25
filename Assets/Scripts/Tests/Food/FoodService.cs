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
            var food = new IFood_Mock(TEST_FOOD_NAME, TEST_FOOD_AMOUNT);

            _foodService.Eat(food, TEST_EAT_AMOUNT);

            float expectedRemaining = TEST_FOOD_AMOUNT - TEST_EAT_AMOUNT;
            Assert.AreEqual(expectedRemaining, food.Amount);
        }

        [Test]
        public void EatFood_EatAmountMoreThanAmount_ThrowsInvalidOperationException()
        {
            var food = new IFood_Mock(TEST_FOOD_NAME, TEST_FOOD_AMOUNT);

            Assert.Throws<InvalidOperationException>(() => _foodService.Eat(food, TEST_FOOD_AMOUNT + TEST_EAT_AMOUNT));
        }

        [Test]
        public void EatFood_EatAmountNegative_ThrowsArgumentException()
        {
            var food = new IFood_Mock(TEST_FOOD_NAME, TEST_FOOD_AMOUNT);

            Assert.Throws<ArgumentException>(() => _foodService.Eat(food, -1));
        }
        #endregion

        #region CookFood
        [Test]
        public void CookFood_IsRaw_False()
        {
            var food = new IFood_Mock(TEST_FOOD_NAME, TEST_FOOD_AMOUNT);

            _foodService.Cook(food);

            Assert.False(food.IsRaw);
        }
        #endregion
    }
}
