using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Food.Tests
{
    public class Container_Tests
    {
        [Test]
        public void Empty_GetContentsVolume_Zero()
        {
            Container container = new Container_Mock();

            Assert.Zero(container.GetContentsVolume());
        }

        [Test]
        public void GetContentsVolume_MatchesItemVolume()
        {
            Container container = new Container_Mock();
            var ingredient = new Ingredient_Mock();
            var volume = 100;
            ingredient.Volume = volume;

            container.Contents.Add(ingredient);

            Assert.AreEqual(volume, container.GetContentsVolume());
        }

        [Test]
        public void MultipleItems_GetContentsVolume_AddsVolumes()
        {
            Container container = new Container_Mock();
            
            var volume1 = 100;
            var ingredient1 = new Ingredient_Mock();
            ingredient1.Volume = volume1;
            container.Contents.Add(ingredient1);

            var volume2 = 100;
            var ingredient2 = new Ingredient_Mock();
            ingredient2.Volume = volume2;
            container.Contents.Add(ingredient2);

            Assert.AreEqual(volume1 + volume2, container.GetContentsVolume());
        }
    }
}
