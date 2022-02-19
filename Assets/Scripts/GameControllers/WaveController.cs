using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController
{
    float m_SpawnCounter = 0;
    WaveData.Wave m_SpawnsPerMinute;
    public void SetSpawnWaveOverTime(WaveData.Wave wave)
    {
        m_SpawnsPerMinute = wave;
    }

    public void UpdateTime(float dt)
    {
        if (m_SpawnsPerMinute != null)
        {
            var gc = Services.Find<GameController>();
            m_SpawnCounter += dt * m_SpawnsPerMinute.Amount / 60;
            while (m_SpawnCounter >= 1)
            {
                var e = m_SpawnsPerMinute.Spawns[Random.Range(0, m_SpawnsPerMinute.Spawns.Count)];
                gc.SpawnEnemy(e);
                m_SpawnCounter--;
            }
        }
    }

    public void SpawnEnemyGroup(WaveData.Wave wave)
    {
        var gc = Services.Find<GameController>();
        for(int n=0;n<wave.Amount;n++)
        {
            var e = wave.Spawns[Random.Range(0, wave.Spawns.Count)];
            gc.SpawnEnemy(e);
        }
    }
}
