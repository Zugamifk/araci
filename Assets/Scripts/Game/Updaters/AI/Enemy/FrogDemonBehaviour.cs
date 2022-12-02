using StateMachines;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogDemonBehaviour : StateMachineAgentBehaviour
{
    public FrogDemonBehaviour(Guid id) : base(id)
    {
        _aiState = new StateMachine(new IdleEnemyState(id));
    }
    public override void Update(GameModel model)
    {
        _aiState.Update();
    }
}
