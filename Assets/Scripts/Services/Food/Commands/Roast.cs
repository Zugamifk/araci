using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Food
{
    public class Roast : PreparationStep
    {
        static RoastProcess _roastProcess = new();

        public override void Do(PreparationContext context)
        {
            foreach (var ingredient in context.Container.Contents)
            {
                if (ingredient is Cookable)
                {
                    _roastProcess.Process(ingredient);
                }
            }
        }
    }
}