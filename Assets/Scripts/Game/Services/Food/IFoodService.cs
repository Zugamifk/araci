using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Food
{
    public interface IFoodService : IService
    {
        void Heat(IFood food, float externalTemperature, float time);
    }
}