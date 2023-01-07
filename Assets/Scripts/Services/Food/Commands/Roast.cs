using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Food
{
    public class Roast : PreparationStep
    {
        public override void Do(PreparationContext context)
        {
            foreach (var ingredient in context.Container.Contents)
            {
                if (ingredient is Cookable cookable)
                {
                    RoastIngredient(cookable);
                }
            }
        }

        void RoastIngredient(Cookable cookable)
        {
            switch ((cookable.CookState, cookable.Moisture))
            {
                case (CookState.Raw, MoistureState.Dry):
                    cookable.CookState = CookState.Burnt;
                    break;
                case (CookState.Raw, MoistureState.Moist):
                    cookable.CookState = CookState.Cooked;
                    break;
                case (CookState.Raw, MoistureState.Saturated):
                    cookable.CookState = CookState.Cooked;
                    break;
                case (CookState.Cooked, MoistureState.Dry):
                    cookable.CookState = CookState.Burnt;
                    break;
                case (CookState.Cooked, MoistureState.Moist):
                    cookable.Moisture = MoistureState.Dry;
                    break;
                case (CookState.Cooked, MoistureState.Saturated):
                    cookable.Moisture = MoistureState.Moist;
                    break;
                case (CookState.Burnt, MoistureState.Dry):
                    break;
                case (CookState.Burnt, MoistureState.Moist):
                    cookable.Moisture = MoistureState.Dry;
                    break;
                case (CookState.Burnt, MoistureState.Saturated):
                    cookable.Moisture = MoistureState.Moist;
                    break;
            }
        }
    }
}