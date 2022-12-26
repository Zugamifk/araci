using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Food.Tests
{
    public class ICookable_Mock : Ingredient, ICookable
    {
        public CookState CookState { get; set; }
    }
}
