using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DropTable : ScriptableObject
{
    [System.Serializable]
    public class Drop
    {
        public Pickup Item;
        public float DropWeight;
    }

    public Item[] Items;

    public float PickupChance = .1f;

    public Drop[] Drops;

    public ExperienceGem[] ExperienceGems;
}
