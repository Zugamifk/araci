using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarrativeStartZone : NarrativeTriggerZone
{
    protected override Color BoundsColor { get; } = new Color(1,1,0);

    protected override void OnEnter()
    {
        Debug.Log("Triggered");
        //Game.Do(new StartNarrative(narrativeKey));
    }
}
