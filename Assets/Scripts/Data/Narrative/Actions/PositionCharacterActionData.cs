using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Narrative
{
    [CreateAssetMenu(menuName ="Narratives/Actions/Position Character")]
    public class PositionCharacterActionData : NarrativeActionData
    {
        [SerializeField]
        string characterKey;
        [SerializeField]
        string positionKey;

        public string Character => characterKey;
        public string Location => positionKey;
    }
}