using System.Collections;
using System.Collections.Generic;
using Core;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Fluids.Tests
{
    public class FluidMixtureTests
    {
        [Test]
        public void Constructor_ZeroMeasure()
        {
            var mixture = new FluidMixture();
            Assert.AreEqual(0, mixture.Measure.Value);
        }

        [Test]
        public void EmptyFluid_DoesNotContainFluid()
        {
            var mixture = new FluidMixture();
            Assert.IsFalse(mixture.ContainsFluid<TestFluid>());
        }

        [Test]
        public void GetFluid_Empty_ReturnsMeasureZero()
        {
            var mixture = new FluidMixture();
            var fluid = mixture.GetFluid<TestFluid>();
            Assert.That(fluid.Measure.Value, Is.EqualTo(0));
        }

        [Test]
        public void GetFluid_Added_ReturnsAddedFluidWithMeasure()
        {
            var mixture = new FluidMixture();
            var toAdd = new TestFluid(10);
            mixture.AddFluid(toAdd);
            var fluid = mixture.GetFluid<TestFluid>();
            Assert.That(fluid.Measure.Value, Is.EqualTo(10));
        }

        [Test]
        public void AddFluid_IncreasesAmount()
        {
            var mixture = new FluidMixture();
            var fluid = new TestFluid(10);
            mixture.AddFluid(fluid);
            Assert.AreEqual(10, mixture.Measure.Value);
        }

        [Test]
        public void AddFluid_AddsFluid()
        {
            var mixture = new FluidMixture();
            var fluid = new TestFluid(10);
            mixture.AddFluid(fluid);
            Assert.IsTrue(mixture.ContainsFluid<TestFluid>());
        }

        [Test]
        public void AddFluid_AlreadyContains_IncreasesFluidAmount()
        {
            float measure = 10;
            var mixture = new FluidMixture();
            var fluid = new TestFluid(measure);
            mixture.AddFluid(fluid);
            mixture.AddFluid(fluid);

            fluid = mixture.GetFluid<TestFluid>();
            Assert.That(fluid.Measure.Value, Is.EqualTo(measure * 2));
        }

        [Test]
        public void AddFluid_AlreadyContains_IncreasesMixtureAmount()
        {
            float measure = 10;
            var mixture = new FluidMixture();
            var fluid = new TestFluid(measure);
            mixture.AddFluid(fluid);
            mixture.AddFluid(fluid);

            fluid = mixture.GetFluid<TestFluid>();
            Assert.That(mixture.Measure.Value, Is.EqualTo(measure * 2));
        }

        [Test]
        public void Addfluid_TypeIFluid_ThrowArgumentException()
        {
            var mixture = new FluidMixture();
            IFluid fluid = new TestFluid();
            Assert.That(()=>mixture.AddFluid(fluid), Throws.ArgumentException);
        }
    }
}
