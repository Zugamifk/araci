using Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Food
{
    public interface IFoodModel
    {
        string Name { get; }
        float Amount { get; }
    }
}
