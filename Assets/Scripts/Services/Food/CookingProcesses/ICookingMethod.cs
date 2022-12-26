using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Food
{
    public interface IProcess
    {
        void Process(Ingredient ingredient);
    }
}