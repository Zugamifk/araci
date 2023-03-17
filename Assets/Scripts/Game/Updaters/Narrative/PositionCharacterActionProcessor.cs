using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Narrative
{
    public class PositionCharacterActionProcessor : NarrativeActionProcessor
    {
        public override void OnStart(GameModel gameModel, NarrativeModel narrativeModel, NarrativeActionData data)
        {
            var spawnData = (PositionCharacterActionData)data;
            var id = gameModel.UniqueKeyToId[spawnData.Character];
            var location = gameModel.MapLocations[spawnData.Location];
            Game.Do(new SetCharacterPosition(id, location));

            IsFinished = true;
        }
    }
}