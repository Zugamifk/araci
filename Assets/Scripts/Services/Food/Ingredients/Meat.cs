using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Food
{
    public abstract class Meat : Ingredient, ICookable
    {
        public CookState CookState { get; set; }
    }
}