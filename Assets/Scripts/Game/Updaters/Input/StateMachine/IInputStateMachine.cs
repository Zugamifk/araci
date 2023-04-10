using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInputStateMachine
{
    void SetState(InputState state);
    void PushState(InputState state);
    void PopState();
}
