using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartDialog : ICommand
{
    string dialogKey;
    string targetKey;

    public StartDialog(string dialogKey, string target)
    {
        this.dialogKey = dialogKey;
        this.targetKey = target;
    }

    public void Execute(GameModel model)
    {
        var dialogCollection = DataService.GetData<DialogDataCollection>();
        var dialogData = dialogCollection.Get(dialogKey);
        var dialogModel = new DialogModel()
        {
            Key = dialogKey,
            SpeakerKey = targetKey,
            CurrentLineIndex = 0,
            CurrentLine = dialogData.Lines[0]
        };
        model.Dialog = dialogModel;
    }
}
