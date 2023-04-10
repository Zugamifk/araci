using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Input
{
    public class DialogState : InputState
    {
        public override void Update(IInputStateMachine inputStateMachine)
        {
            if(Game.Model.Dialog == null)
            {
                inputStateMachine.PopState();
                return;
            }
        }
    }
}
