using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Food.Tests
{
    public class ICookable_Mock : Cookable
    {
        public string MockKey;
        public override string Key => MockKey;
    }
}
