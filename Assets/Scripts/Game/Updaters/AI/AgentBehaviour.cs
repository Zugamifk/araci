using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AgentBehaviour
{
    public Guid Id;
    public abstract void Update(GameModel model, ICharacterModel agent);
}
