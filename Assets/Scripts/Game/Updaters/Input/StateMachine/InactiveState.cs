using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Input
{
    public class InactiveState : InputState
    {
        public override InputState Update()
        {
            // Do nothing;

            return this;
        }
    }
}