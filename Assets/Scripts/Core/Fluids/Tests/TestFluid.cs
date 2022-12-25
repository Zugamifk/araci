using Core;
using Fluids;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fluids.Tests
{
    internal readonly struct TestFluid : IFluid
    {
        public Measure Measure { get; }
        public TestFluid(Measure m) => Measure = m;

        public IFluid CombineWith(IFluid other) => new TestFluid(Measure + other.Measure);
    }
}
