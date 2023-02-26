using Narrative;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemiesStateBehaviour : NarrativeActionProcessor
{
    public override void OnUpdate(GameModel gameModel, NarrativeModel narrativeModel, NarrativeActionData data)
    {
        var spawnEnemiesData = (SpawnEnemiesActionData)data;
        for (int i = 0; i < spawnEnemiesData.Count; i++)
        {
            Game.Do(new SpawnEnemy(spawnEnemiesData.EnemyData.Key, spawnEnemiesData.SpawnName));
        }
        IsFinished = true;
    }
}
