using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartDialog : ICommand
{
    string dialogKey;
    string speakerKey;

    public StartDialog(string dialogKey, string speakerKey)
    {
        this.dialogKey = dialogKey;
        this.speakerKey = speakerKey;
    }

    public void Execute(GameModel model)
    {
        var dialogCollection = DataService.GetData<DialogDataCollection>();
        var dialogData = dialogCollection.Get(dialogKey);
        var speakerId = model.UniqueKeyToId[speakerKey];
        var dialogModel = new DialogModel()
        {
            Key = dialogKey,
            SpeakerId = speakerId,
            CurrentLineIndex = 0,
            CurrentLine = dialogData.Lines[0]
        };
        model.Dialog = dialogModel;
    }
}
