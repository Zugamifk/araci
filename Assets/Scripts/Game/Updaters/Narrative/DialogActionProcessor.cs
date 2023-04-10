using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Narrative
{
    public class DialogActionProcessor : NarrativeActionProcessor
    {
        public override void OnStart(GameModel gameModel, NarrativeModel narrativeModel, NarrativeActionData data)
        {
            var dialogActionData = (DialogActionData)data;
            var dialogCollection = DataService.GetData<DialogDataCollection>();
            var dialog = dialogCollection.Get(dialogActionData.DialogKey);
            Game.Do(new StartDialog(dialogActionData.DialogKey, dialog.SpeakerKey));
        }
    }
}