using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Food
{
    public interface IHeatable
    {
        float Weight { get; set; }
        float Volume { get; set; }
    }
}