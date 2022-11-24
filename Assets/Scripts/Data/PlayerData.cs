using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : ScriptableObject
{
    [System.Serializable]
    public struct MeterLevel
    {
        public float ChargeNeeded;
    }
    [SerializeField]
    MeterLevel[] _meterLevels;

    public MeterLevel[] MeterLevels => _meterLevels;
}
