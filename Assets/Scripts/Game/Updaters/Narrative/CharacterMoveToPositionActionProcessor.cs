using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Narrative
{
    public class CharacterMoveToPositionActionProcessor : NarrativeActionProcessor
    {
        ICharacterModel _characterModel;
        IPositionModel _positionModel;
        Vector2 _destination;

        public override void OnStart(GameModel gameModel, NarrativeModel narrativeModel, NarrativeActionData data)
        {
            var spawnData = (CharacterMoveToPositionActionData)data;
            var id = gameModel.UniqueKeyToId[spawnData.Character];
            _destination = gameModel.MapLocations[spawnData.Location];
            _characterModel = gameModel.Characters.GetItem(id);
            _positionModel = gameModel.Positions.GetItem(id);
            var direction = (_destination - _positionModel.Position.Value).normalized;
            Game.Do(new MoveCharacter(id, direction));
        }

        public override void OnUpdate(GameModel gameModel, NarrativeModel narrativeModel, NarrativeActionData data)
        {
            if(_positionModel.IsApproximateAtPosition(_destination))
            {
                IsFinished = true;
            } else
            {
                var direction = (_destination - _positionModel.Position.Value).normalized;
                Game.Do(new MoveCharacter(_characterModel.Id, direction));
            }
        }

        public override void OnFinish(GameModel gameModel, NarrativeModel narrativeModel, NarrativeActionData data)
        {
            Game.Do(new MoveCharacter(_characterModel.Id, Vector3.zero));
        }
    }
}