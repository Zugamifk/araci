using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Food
{
    public interface IIngredient : IHeatable
    {
        string Name { get; set; }
    }
}