using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Food.Tests
{
    public class ICookable_Mock : ICookable
    {
        public CookState CookState { get; set; }
        public MoistureState Moisture { get; set; }
    }
}
