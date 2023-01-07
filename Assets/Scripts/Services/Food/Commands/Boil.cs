using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace Food
{
    public class Boil : PreparationStep
    {
        public override void Do(PreparationContext context)
        {
            if(context.Container.Contents.Count == 0)
            {
                throw new System.InvalidOperationException($"Can't boil with an empty container!");
            }
        }
    }
}