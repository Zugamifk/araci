using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveData : ScriptableObject
{
    public enum EWaveType
    {
        Instant,
        SpawnPerMinute
    }

    [System.Serializable]
    public class Wave
    {
        public EWaveType WaveType;
        public Vector2Int Time;
        public List<Enemy> Spawns = new List<Enemy>();
        public float Amount;

        public int TimeInSeconds => Time.x * 60 + Time.y;
    }

    [SerializeField]
    public List<Wave> Waves = new List<Wave>();
}
