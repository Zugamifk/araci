using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIService : IUIService
{
    Dictionary<string, string> hotkeyToWindowKey = new();
    public IEnumerable<string> GetPanelHotkeys()
    {
        return hotkeyToWindowKey.Keys;
    }

    public void PressedPanelHotkey(string key)
    {
        var window = hotkeyToWindowKey[key];
        Game.Do(new ToggleUIPanel(window));
    }

    public void RegisterHotkey(string key, string windowKey)
    {
        hotkeyToWindowKey.Add(key, windowKey);
    }
}
