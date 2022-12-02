using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AgentBehaviour
{
    protected Guid _id;
    public AgentBehaviour(Guid id)
    {
        _id = id;
    }

    public abstract void Update(GameModel model);
}
