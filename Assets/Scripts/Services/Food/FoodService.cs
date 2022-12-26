using Codice.Client.Common;
using Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Food
{
    public class FoodService : IFoodService
    {
        public void Cook(ICookable cookable, ICookingMethod method)
        {
            method.Cook(cookable);
        }
    }
}