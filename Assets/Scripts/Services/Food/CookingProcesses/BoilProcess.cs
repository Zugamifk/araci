using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Food
{
    public class BoilProcess : IProcess
    {
        public void Process(Ingredient ingredient)
        {
            if (ingredient is not Cookable cookable)
            {
                throw new System.InvalidOperationException($"{ingredient.Name} is not cookable!");
            }

            switch ((cookable.CookState, cookable.Moisture))
            {
                case (CookState.Raw, MoistureState.Dry):
                    cookable.Moisture = MoistureState.Moist;
                    break;
                case (CookState.Raw, MoistureState.Moist):
                    cookable.Moisture = MoistureState.Saturated;
                    break;
                case (CookState.Raw, MoistureState.Saturated):
                    cookable.CookState = CookState.Cooked;
                    break;
                case (CookState.Cooked, MoistureState.Dry):
                    cookable.Moisture = MoistureState.Moist;
                    break;
                case (CookState.Cooked, MoistureState.Moist):
                    cookable.Moisture = MoistureState.Saturated;
                    break;
                case (CookState.Cooked, MoistureState.Saturated):
                    break;
                case (CookState.Burnt, MoistureState.Dry):
                    cookable.Moisture = MoistureState.Moist;
                    break;
                case (CookState.Burnt, MoistureState.Moist):
                    cookable.Moisture = MoistureState.Saturated;
                    break;
                case (CookState.Burnt, MoistureState.Saturated):
                    break;
            }
        }
    }
}