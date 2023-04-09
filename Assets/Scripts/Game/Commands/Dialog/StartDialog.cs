using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartDialog : ICommand
{
    string dialogKey;
    string targetKey;

    public StartDialog(string dialogKey, string targetKey)
    {
        this.dialogKey = dialogKey;
        this.targetKey = targetKey;
    }

    public void Execute(GameModel model)
    {
        var dialogCollection = DataService.GetData<DialogDataCollection>();
        var dialogData = dialogCollection.Get(dialogKey);
        var targetCharacterId = model.UniqueKeyToId[targetKey];
        var dialogModel = new DialogModel()
        {
            Key = dialogKey,
            SpeakerId = targetCharacterId,
            CurrentLineIndex = 0,
            CurrentLine = dialogData.Lines[0]
        };
        model.Dialog = dialogModel;
    }
}
