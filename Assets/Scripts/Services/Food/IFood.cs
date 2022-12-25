using Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Food
{
    public interface IFood
    {
        string Name { get; set; }
        float Amount { get; set; }
        bool IsRaw { get; set; }
    }
}