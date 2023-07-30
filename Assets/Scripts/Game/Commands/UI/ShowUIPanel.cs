using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleUIPanel : ICommand
{
    string windowKey;

    public ToggleUIPanel(string windowKey)
    {
        this.windowKey = windowKey;
    }

    public void Execute(GameModel model)
    {
        if(model.UI.CurrentOpenWindow.Value == windowKey)
        {
            model.UI.CurrentOpenWindow.Value = string.Empty;
        } else
        {
            model.UI.CurrentOpenWindow.Value = windowKey;
        }
    }
}
