using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Food
{
    public class Roast : ICookingMethod
    {
        public void Cook(ICookable cookable)
        {
            switch (cookable.CookState)
            {
                case CookState.Raw:
                    cookable.CookState = CookState.Cooked;
                    break;
                case CookState.Cooked:
                    cookable.CookState = CookState.Burnt;
                    break;
                default:
                    break;
            }

            switch (cookable.Moisture)
            {
                case MoistureState.Moist:
                    cookable.Moisture = MoistureState.Dry;
                    break;
                case MoistureState.Saturated:
                    cookable.Moisture = MoistureState.Moist;
                    break;
                default: break;
            }
        }
    }
}