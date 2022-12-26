using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Food
{
    public abstract class PreparationStep
    {
        public abstract void Do(PreparationContext context);
    }
}