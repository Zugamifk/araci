using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceGem : Pickup
{
    [SerializeField]
    int m_Value;

    public override void PickupItem()
    {
        Services.Find<PlayerController>().GainExperience(m_Value);
    }
}
