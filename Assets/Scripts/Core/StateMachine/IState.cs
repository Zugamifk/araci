using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachines
{
    public interface IState
    {
        IState UpdateState();
        void EnterState();
    }
}