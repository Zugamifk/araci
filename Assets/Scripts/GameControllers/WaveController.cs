using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController
{
    WaveData.Wave m_SpawnsPerMinute;
    List<float> m_SpawnTimers = new List<float>();
    
    public void SetWaveData(WaveData data)
    {
        var tc = Services.Find<TimeController>();
        foreach (var w in data.Waves)
        {
            switch (w.WaveType)
            {
                case WaveData.EWaveType.Instant:
                    tc.ScheduleEvent(w.TimeInSeconds, _ => SpawnEnemyGroup(w));
                    break;
                case WaveData.EWaveType.SpawnPerMinute:
                    tc.ScheduleEvent(w.TimeInSeconds, _ => SetSpawnWaveOverTime(w));
                    break;
                default:
                    break;
            }
        }
    }
    
    public void SetSpawnWaveOverTime(WaveData.Wave wave)
    {
        m_SpawnsPerMinute = wave;
        m_SpawnTimers.Clear();
    }

    public void UpdateTime(float dt)
    {
        if (m_SpawnsPerMinute != null)
        {
            var gc = Services.Find<GameController>();
            for(int i=0;i<m_SpawnsPerMinute.Spawns.Count;i++)
            {
                if(i >= m_SpawnTimers.Count)
                {
                    m_SpawnTimers.Add(0);
                }

                var s = m_SpawnsPerMinute.Spawns[i];
                var t = m_SpawnTimers[i];
                t += dt * s.Amount / 60; ;
                while(t > 0)
                {
                    gc.SpawnEnemy(s.Enemy);
                    t--;
                }
                m_SpawnTimers[i] = t;
            }
        }
    }

    public void SpawnEnemyGroup(WaveData.Wave wave)
    {
        var gc = Services.Find<GameController>();
        for (int i = 0; i < wave.Spawns.Count; i++)
        {
            var s = wave.Spawns[i];
            for (int n = 0; n < s.Amount; n++)
            {
                gc.SpawnEnemy(s.Enemy);
            }
        }
    }
}
