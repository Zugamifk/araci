using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Food
{
    public class Chicken : Meat
    {
        public override string Key => "Chicken";

        public Chicken()
        {
            CookState = CookState.Raw;
            Moisture = MoistureState.Moist;
        }
    }
}