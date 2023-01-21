using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InfoPane
{
    public string TabTitle;

    public InfoPane(string tabTitle)
    {
        TabTitle = tabTitle;
    }

    public abstract void DrawContents();
}
