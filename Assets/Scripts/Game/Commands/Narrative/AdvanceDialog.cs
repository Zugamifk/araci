using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvanceDialog : ICommand
{
    public void Execute(GameModel model)
    {
        var dialogCollection = DataService.GetData<DialogDataCollection>();
        var dialogData = dialogCollection.Get(model.Dialog.Key);

        model.Dialog.CurrentLineIndex++;
        if(model.Dialog.CurrentLineIndex < dialogData.Lines.Length)
        {
            model.Dialog.CurrentLine = dialogData.Lines[model.Dialog.CurrentLineIndex];
        } else
        {
            model.Dialog = null;
        }
    }
}
