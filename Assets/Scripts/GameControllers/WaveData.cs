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
    public class EnemySpawn
    {
        public Enemy Enemy;
        public float Amount;
    }

    [System.Serializable]
    public class Wave
    {
        public EWaveType WaveType;
        public Vector2Int Time;
        public List<EnemySpawn> Spawns = new List<EnemySpawn>();

        public int TimeInSeconds => Time.x * 60 + Time.y;
    }

    [SerializeField]
    public List<Wave> Waves = new List<Wave>();
}
