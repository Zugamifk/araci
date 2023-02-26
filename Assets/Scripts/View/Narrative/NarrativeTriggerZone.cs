using Codice.Client.BaseCommands;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NarrativeTriggerZone : TriggerZone
{
    [SerializeField]
    protected string narrativeKey;
    [SerializeField]
    protected string zoneKey;
}
