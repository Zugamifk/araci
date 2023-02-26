using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Narrative
{
    [CreateAssetMenu(menuName ="Narratives/Actions/Spawn Enemies")]
    public class SpawnEnemiesActionData : NarrativeActionData
    {
        public EnemyData EnemyData;
        public string SpawnName;
        public int Count;
    }
}