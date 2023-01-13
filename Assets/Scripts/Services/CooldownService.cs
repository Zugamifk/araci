using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class CooldownService : ICooldownService
{
    public bool IsReady(ICooldownModel cooldownModel)
    {
        return cooldownModel.ReadyTime <= Game.Model.Time.Time;
    }

    public void StartCooldown(CooldownModel cooldownModel)
    {
        cooldownModel.ReadyTime = Game.Model.Time.Time + cooldownModel.Duration;
    }
}
