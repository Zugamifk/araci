using Narrative;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemiesStateBehaviour : NarrativeStateBehaviour<SpawnEnemiesState>
{
    public override void Update()
    {
        for(int i = 0; i < _data.Count; i++)
        {
            Game.Do(new SpawnEnemy(_data.EnemyData.Name, _data.SpawnName));
        }
        IsDone = true;
    }
}
