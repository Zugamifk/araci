using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Food
{
    public enum Temperature
    {
        // belolw freezing, around -5-10
        Freezing,

        // just above freezing 5-10C
        Cold,
        
        // around body temperature 30-40C
        Warm,

        // around cooking temperature, 50-60C
        Hot,

        // above boiling, 100-110
        Scalding
    }
}