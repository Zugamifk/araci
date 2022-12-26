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

        [Test]
        public void Cook_Roast_Raw_IsCooked()
        {
            var cookable = new ICookable_Mock();
            cookable.CookState = CookState.Raw;

            _foodService.Cook(cookable, new Roast());

            Assert.AreEqual(CookState.Cooked, cookable.CookState);
        }

        [Test]
        public void Cook_Roast_Cooked_IsBurnt()
        {
            var cookable = new ICookable_Mock();
            cookable.CookState = CookState.Cooked;

            _foodService.Cook(cookable, new Roast());

            Assert.AreEqual(CookState.Burnt, cookable.CookState);
        }

        [Test]
        public void Cook_Roast_Burnt_IsBurnt()
        {
            var cookable = new ICookable_Mock();
            cookable.CookState = CookState.Burnt;

            _foodService.Cook(cookable, new Roast());

            Assert.AreEqual(CookState.Burnt, cookable.CookState);
        }
    }
}
