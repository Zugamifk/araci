using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System;
using Core;

namespace Core.Tests
{
    public class MeasureTests
    {
        #region New
        [Test]
        public void New_EmptyConstructor_MeasureZero()
        {
            var measure = new Measure();
            Assert.AreEqual(measure.Value, 0);
        }

        [Test]
        public void New_AmountEqualsGiven()
        {
            var measure = new Measure(100);
            Assert.AreEqual(measure.Value, 100);
        }

        [Test]
        public void New_NegativeAmount_Throws()
        {
            Assert.Throws<ArgumentException>(() => new Measure(-100));
        }
        #endregion

        #region IsEmpty
        [Test]
        public void IsEmpty_ZeroMeasure_True()
        {
            var measure = new Measure(0);
            Assert.That(measure.IsEmpty, Is.True);
        }

        [Test]
        public void IsEmpty_OneMeasure_False()
        {
            var measure = new Measure(1);
            Assert.That(measure.IsEmpty, Is.False);
        }

        #endregion

        #region Cast
        [Test]
        public void ImplicitCast_FromFloat_ValueMatches()
        {
            Measure measure = 1;
            Assert.That(measure.Value, Is.EqualTo(1));
        }

        [Test]
        public void ImplicitCast_FromNegativeFloat_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Measure invalid = -1;
            });
        }

        [Test]
        public void ImplicitCast_ToFloat_ValueMatches()
        {
            Measure measure = 1;
            float toFloat = measure;
            Assert.That(toFloat, Is.EqualTo(1));
        }

        #endregion

        #region Combine
        [Test]
        public void AddMeasure_AddsAmounts(
            [Values(0, 1, 0.5f, 1000)] float a,
            [Values(0, 1, 0.5f, 1000)] float b)
        {
            var result = new Measure(a) + new Measure(b);
            Assert.That(result.Value, Is.EqualTo(a + b));
        }

        [Test]
        public void AddFloat_AddsAmounts(
            [Values(0, 1, 0.5f, 1000)] float a,
            [Values(0, 1, 0.5f, 1000)] float b)
        {
            var ma = new Measure(a);
            var mb = new Measure(b);
            var floatSum = a + b;
            var aPmb = a + mb;
            var maPb = ma + b;
            Assert.AreEqual(aPmb, floatSum);
            Assert.AreEqual(maPb, floatSum);
        }

        [Test]
        public void SubtractMeasure_SubtractsAmounts(
                    [Values(10, 100, 123.456f)] float a,
                    [Values(0, 1, 0.5f)] float b)
        {
            var ma = new Measure(a);
            var mb = new Measure(b);
            var measureResult = ma - mb;
            var floatResult = a - b;
            Assert.That(measureResult.Value, Is.EqualTo(floatResult));
        }

        [Test]
        public void SubtractFloat_SubtractsAmounts(
                    [Values(10, 100, 123.456f)] float a,
                    [Values(0, 1, 0.5f)] float b)
        {
            var ma = new Measure(a);
            var mb = new Measure(b);
            var aMmb = a - mb;
            var maMb = ma - b;
            var floatResult = a - b;
            Assert.AreEqual(aMmb, floatResult);
            Assert.AreEqual(maMb, floatResult);
        }

        [Test]
        public void SubtractMeasure_ThrowsOnNegativeResult()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                var a = new Measure(2);
                var b = new Measure(5);
                var sum = a - b;
            });
        }
        #endregion

        #region Scale
        [Test]
        public void MultiplyPercent_ReturnsPercentOfAmount()
        {
            var a = new Measure(10);
            var b = new Percent(50);
            var half = a * b;
            Assert.AreEqual(half.Value, 5);
            half = b * a;
            Assert.AreEqual(half.Value, 5);
        }

        [Test]
        public void MultiplyNumber_ReturnsMeasureWithValueMultiplied(
            [Values(0, 1, Mathf.PI, 1000)] float scalar,
            [Values(0, 1, Mathf.PI, 1000)] float measureValue)
        {
            var measure = new Measure(measureValue);
            var resultMS = measure * scalar;
            var resultSM = scalar * measure;
            var value = scalar * measureValue;
            Assert.That(resultMS, Is.EqualTo(new Measure(value)));
            Assert.That(resultSM, Is.EqualTo(new Measure(value)));
        }
        #endregion

        #region Compare
#pragma warning disable CS1718
        [Test]
        public void LessThan_ComparesValues()
        {
            Measure zero = 0;
            Measure one = 1;
            Assert.IsTrue(zero < one);
            Assert.IsFalse(one < zero);
            Assert.IsFalse(zero < zero);
            Assert.IsFalse(one < one);
        }

        [Test]
        public void LessThanOrEqual_ComparesValues()
        {
            Measure zero = 0;
            Measure one = 1;
            Assert.IsTrue(zero <= one);
            Assert.IsTrue(zero <= zero);
            Assert.IsTrue(one <= one);
            Assert.IsFalse(one <= zero);
        }

        [Test]
        public void GreaterThan_ComparesValues()
        {
            Measure zero = 0;
            Measure one = 1;
            Assert.IsFalse(zero > one);
            Assert.IsTrue(one > zero);
            Assert.IsFalse(zero > zero);
            Assert.IsFalse(one > one);
        }

        [Test]
        public void GreaterThanOrEqual_ComparesValues()
        {
            Measure zero = 0;
            Measure one = 1;
            Assert.IsFalse(zero >= one);
            Assert.IsTrue(zero >= zero);
            Assert.IsTrue(one >= one);
            Assert.IsTrue(one >= zero);
        }

        [Test]
        public void IsEqual_ComparesValues(
            [Values(0,1)] float a,
            [Values(0,1)] float b)
        {
            var resultMM = new Measure(a) == new Measure(b);
            Assert.That(resultMM, Is.EqualTo(a == b));

            var resultFM = a == new Measure(b);
            Assert.That(resultFM, Is.EqualTo(a == b));

            var resultMF = new Measure(a) == b;
            Assert.That(resultMF, Is.EqualTo(a == b));
        }

        [Test]
        public void IsNotEqual_ComparesValues(
            [Values(0, 1)] float a,
            [Values(0, 1)] float b)
        {
            var resultMM = new Measure(a) != new Measure(b);
            Assert.That(resultMM, Is.EqualTo(a != b));

            var resultFM = a != new Measure(b);
            Assert.That(resultFM, Is.EqualTo(a != b));

            var resultMF = new Measure(a) != b;
            Assert.That(resultMF, Is.EqualTo(a != b));
        }
#pragma warning restore CS1718
        #endregion
    }
}
