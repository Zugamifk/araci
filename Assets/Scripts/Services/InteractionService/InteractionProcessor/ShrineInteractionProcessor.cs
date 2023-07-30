using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrineInteractionProcessor : IInteractionProcessor
{
    public void ProcessInteraction(Guid id)
    {
        Game.Do(new UseShrine(id));
    }
}
