using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Narrative {
    [CreateAssetMenu(menuName = "Narratives/Actions/Do Dialog")]
    public class DialogActionData : NarrativeActionData
    {
        [SerializeField]
        KeyAsset dialogKey;

        public string DialogKey => dialogKey;
    }
}