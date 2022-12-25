using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;

namespace Fluids
{
    public interface IFluid
    {
        Measure Measure { get; }
        IFluid CombineWith(IFluid other);
    }
}
