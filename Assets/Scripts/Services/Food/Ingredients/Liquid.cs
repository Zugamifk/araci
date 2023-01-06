using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Food
{
    public abstract class Liquid : Ingredient
    {
        public Temperature Temperature { get; set; }
        

        public void ApplyTemperature(Temperature temperature)
        {
            if(temperature > Temperature)
            {
                IncreaseTemperature();
            } else if(temperature < Temperature)
            {
                DecreaseTemperature();
            }
        }

        private void DecreaseTemperature()
        {
            switch (Temperature)
            {
                case Temperature.Cold:
                    Temperature = Temperature.Freezing;
                    break;
                case Temperature.Warm:
                    Temperature = Temperature.Cold;
                    break;
                case Temperature.Hot:
                    Temperature = Temperature.Warm;
                    break;
                case Temperature.Scalding:
                    Temperature = Temperature.Hot;
                    break;
                default:
                    break;
            }
        }

        public void IncreaseTemperature()
        {
            switch (Temperature)
            {
                case Temperature.Freezing:
                    Temperature = Temperature.Cold;
                    break;
                case Temperature.Cold:
                    Temperature = Temperature.Warm;
                    break;
                case Temperature.Warm:
                    Temperature = Temperature.Hot;
                    break;
                case Temperature.Hot:
                    Temperature = Temperature.Scalding;
                    break;
                case Temperature.Scalding:
                default:
                    break;
            }
        }

        public LiquidState GetLiquidState()
        {
            throw new NotImplementedException();
        }
    }
}