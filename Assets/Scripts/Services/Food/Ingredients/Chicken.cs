using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Food
{
    public class Chicken : Ingredient, ICookable
    {
         public CookState CookState { get; set; }
    }
}