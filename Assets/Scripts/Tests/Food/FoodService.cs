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
            Assert.Throws<ArgumentException>(()=> _foodService.GetFood(TEST_FOOD_NAME, 0));
        }

        [Test]
        public void GetFood_NegativeMeasure_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => _foodService.GetFood(TEST_FOOD_NAME, -1));
        }

        [Test]
        public void EatFood_RemovesAmount()
        {
            var food = _foodService.GetFood(TEST_FOOD_NAME, TEST_FOOD_AMOUNT);

            _foodService.Eat(food, TEST_EAT_AMOUNT);

            float expectedRemaining = TEST_FOOD_AMOUNT - TEST_EAT_AMOUNT;
            Assert.AreEqual(expectedRemaining, food.Amount);
        }
    }
}
