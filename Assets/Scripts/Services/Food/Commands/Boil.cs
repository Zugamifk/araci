using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace Food
{
    public class Boil : PreparationStep
    {
        List<Liquid> liquids = new List<Liquid>();
        List<Cookable> cookables = new List<Cookable>();

        public override void Do(PreparationContext context)
        {
            if(context.Container.Contents.Count == 0)
            {
                throw new System.InvalidOperationException($"Can't boil with an empty container!");
            }

            SplitIngredients(context.Container.Contents);

            if (liquids.Count == 0)
            {
                throw new System.InvalidOperationException($"No liquids in container! Can't boil.");
            }

            BringToBoil();
            CookAllIngredients();
        }

        void SplitIngredients(IEnumerable<Ingredient> ingredients)
        {
            foreach (var i in ingredients)
            {
                if (i is Liquid l)
                {
                    liquids.Add(l);
                }
                else if (i is Cookable c)
                {
                    cookables.Add(c);
                }
            }
        }

        void BringToBoil()
        {
            int s = 1000;
            while (s-- > 0)
            {
                bool allBoiling = true;
                foreach (var l in liquids)
                {
                    l.ApplyTemperature(Temperature.Boiling);
                    allBoiling &= l.GetLiquidState() == LiquidState.Boiling;
                }
                if (allBoiling) break;
            }
        }

        void CookAllIngredients()
        {
            int s = 1000;
            while (s-- > 0)
            {
                bool allCooked = true;
                foreach (var c in cookables)
                {
                    CookIngredient(c);
                    allCooked &= c.CookState == CookState.Cooked;
                }
                if (allCooked) break;
            }
        }

        void CookIngredient(Cookable cookable)
        {
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