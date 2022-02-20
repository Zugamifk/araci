using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : Pickup
{
    [SerializeField]
    int m_HealAmount;

    public override void PickupItem()
    {
        Services.Find<PlayerController>().Heal(m_HealAmount);
    }
}
