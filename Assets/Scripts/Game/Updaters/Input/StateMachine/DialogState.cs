using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Input;

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

            if (GetKeyDown(KeyCode.E))
            {
                Game.Do(new AdvanceDialog());
            }
        }
    }
}
