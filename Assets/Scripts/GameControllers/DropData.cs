using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropData : ScriptableObject
{
    [System.Serializable]
    public class Drop
    {
        public Pickup Item;
        public float DropWeight;
    }

    public Drop[] Drops;
}
