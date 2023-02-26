using Narrative;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemiesStateBehaviour : NarrativeStateBehaviour
{
    public override void OnUpdateState(GameModel gameModel, NarrativeModel narrativeModel, NarrativeStateData data)
    {
        var spawnEnemiesData = (SpawnEnemiesStateData)data;
        for (int i = 0; i < spawnEnemiesData.Count; i++)
        {
            Game.Do(new SpawnEnemy(spawnEnemiesData.EnemyData.Key, spawnEnemiesData.SpawnName));
        }
        IsFinished = true;
    }
}
