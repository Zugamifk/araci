using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Food.Tests
{
    public class IFood_Mock : IFood
    {
        public string Name { get; set; }

        public float Amount { get; set; }

        public bool IsRaw { get; set; }

        public IFood_Mock(string name, float amount)
        {
            Name = name;
            Amount = amount;
            IsRaw = true;
        }
    }
}
