using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUIService : IService
{
    void RegisterHotkey(string key, string windowKey);
    IEnumerable<string> GetPanelHotkeys();
    void PressedPanelHotkey(string key);
}
