using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;

namespace Fluids
{
    public class FluidMixture : IFluid
    {
        Dictionary<Type, IFluid> _fluidToMeasure = new Dictionary<Type, IFluid>();
        Measure _measure;
        public Measure Measure { get => _measure; }

        public void AddFluid<TFluid>(TFluid fluid)
            where TFluid : IFluid
        {
            if (typeof(TFluid) == typeof(IFluid))
            {
                throw new ArgumentException($"Passed a fluid of type IFluid! Specify type when calling");
            }
            AddFluid(fluid, typeof(TFluid));
        }

        public IFluid CombineWith(IFluid other)
        {
            throw new NotImplementedException();
        }

        public bool ContainsFluid<TFluid>() => _fluidToMeasure.ContainsKey(typeof(TFluid));

        public TFluid GetFluid<TFluid>()
            where TFluid : IFluid
        {
            if (_fluidToMeasure.TryGetValue(typeof(TFluid), out IFluid fluid))
            {
                return (TFluid)fluid;
            }
            else
            {
                return default;
            }
        }

        void AddFluid(IFluid fluid, Type type)
        {
            if (_fluidToMeasure.TryGetValue(type, out IFluid contained))
            {
                var combined = fluid.CombineWith(contained);
                _fluidToMeasure[type] = combined;
            }
            else
            {
                _fluidToMeasure.Add(type, fluid);
            }
            _measure += fluid.Measure;
        }
    }
}
