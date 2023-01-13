using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : ScriptableObject, IRegisteredData
{
    [System.Serializable]
    public struct MeterLevel
    {
        public float ChargeNeeded;
    }
    [SerializeField]
    MeterLevel[] _meterLevels;
    [SerializeField]
    float _dashSpeed;
    [SerializeField]
    float _dashDuration;

    public MeterLevel[] MeterLevels => _meterLevels;
    public float DashSpeed => _dashSpeed;
    public float DashDuration => _dashDuration;
}
