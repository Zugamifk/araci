using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Food
{
    public interface ICookingMethod
    {
        void Cook(ICookable cookable);
    }
}