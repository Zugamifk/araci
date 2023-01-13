using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICooldownService : IService
{
    public bool IsReady(ICooldownModel cooldown);
    public void StartCooldown(CooldownModel cooldown);
}
