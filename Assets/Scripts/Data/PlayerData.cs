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
    MeterLevel[] meterLevels;
    [SerializeField]
    float dashSpeed;
    [SerializeField]
    float dashDuration;
    [SerializeField]
    float dashCooldown;

    public MeterLevel[] MeterLevels => meterLevels;
    public float DashSpeed => dashSpeed;
    public float DashDuration => dashDuration;
    public float DashCooldown => dashCooldown;
}
