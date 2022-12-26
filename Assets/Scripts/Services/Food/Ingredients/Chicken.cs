using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Food
{
    public class Chicken : IIngredient, ICookable
    {
        public string Name => "Chicken";

        public CookState CookState { get; set; }
    }
}