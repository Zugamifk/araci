using StateMachines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachines
{
    public class StateMachine
    {
        IState _current;

        public StateMachine(IState start)
        {
            _current = start;
        }

        public void Update()
        {
            var next = _current.UpdateState();
            if (next != _current)
            {
                _current = next;
                next.EnterState();
            }
        }
    }
}
