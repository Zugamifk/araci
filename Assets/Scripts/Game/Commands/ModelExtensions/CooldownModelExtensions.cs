using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CooldownModelExtensions
{
    public static bool IsReady(this ICooldownModel cooldown)
    {
        return cooldown.ReadyTime <= Game.Model.Time.Time;
    }
}
