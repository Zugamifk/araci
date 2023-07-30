using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarvestableActionProcessor : IInteractionProcessor
{
    public void ProcessInteraction(Guid id)
    {
        Game.Do(new Harvest(id));     
    }
}
