using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Narrative
{
    [CreateAssetMenu(menuName = "Narratives/Actions/Move Character")]
    public class CharacterMoveToPositionActionData : NarrativeActionData
    {
        [SerializeField]
        KeyAsset characterKey;
        [SerializeField]
        KeyAsset positionKey;

        public string Character => characterKey;
        public string Location => positionKey;
    }
}