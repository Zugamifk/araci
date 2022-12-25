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

        #region GetFood
        [Test]
        public void GetFood_ReturnsNotNull()
        {
            var food = _foodService.GetFood(TEST_FOOD_NAME, TEST_FOOD_AMOUNT);

            Assert.NotNull(food);
        }

        [Test]
        public void GetFood_HasGivenName()
        {
            var food = _foodService.GetFood(TEST_FOOD_NAME, TEST_FOOD_AMOUNT);

            Assert.AreEqual(TEST_FOOD_NAME, food.Name);
        }

        [Test]
        public void GetFood_EmptyName_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => _foodService.GetFood(string.Empty, TEST_FOOD_AMOUNT));
        }

        [Test]
        public void GetFood_HasGivenMeasure()
        {
            var food = _foodService.GetFood(TEST_FOOD_NAME, TEST_FOOD_AMOUNT);

            Assert.AreEqual(TEST_FOOD_AMOUNT, food.Amount);
        }

        [Test]
        public void GetFood_ZeroMeasure_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => _foodService.GetFood(TEST_FOOD_NAME, 0));
        }

        [Test]
        public void GetFood_NegativeMeasure_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => _foodService.GetFood(TEST_FOOD_NAME, -1));
        }

        [Test]
        public void GetFood_IsRaw_True()
        {
            var food = _foodService.GetFood(TEST_FOOD_NAME, TEST_FOOD_AMOUNT);
            Assert.IsTrue(food.IsRaw);
        }
        #endregion

        #region EatFood
        [Test]
        public void EatFood_RemovesAmount()
        {
            var food = _foodService.GetFood(TEST_FOOD_NAME, TEST_FOOD_AMOUNT);

            var eatenFood = _foodService.Eat(food, TEST_EAT_AMOUNT);

            float expectedRemaining = TEST_FOOD_AMOUNT - TEST_EAT_AMOUNT;
            Assert.AreEqual(expectedRemaining, eatenFood.Amount);
        }

        [Test]
        public void EatFood_EatAmountMoreThanAmount_ThrowsInvalidOperationException()
        {
            var food = _foodService.GetFood(TEST_FOOD_NAME, TEST_FOOD_AMOUNT);

            Assert.Throws<InvalidOperationException>(() => _foodService.Eat(food, TEST_FOOD_AMOUNT + TEST_EAT_AMOUNT));
        }

        [Test]
        public void EatFood_EatAmountNegative_ThrowsArgumentException()
        {
            var food = _foodService.GetFood(TEST_FOOD_NAME, TEST_FOOD_AMOUNT);

            Assert.Throws<ArgumentException>(() => _foodService.Eat(food, -1));
        }
        #endregion

        #region CookFood
        [Test]
        public void CookFood_IsRaw_False()
        {
            var food = _foodService.GetFood(TEST_FOOD_NAME, TEST_FOOD_AMOUNT);

            var newFood = _foodService.Cook(food);

            Assert.False(newFood.IsRaw);
        }
        #endregion
    }
}
