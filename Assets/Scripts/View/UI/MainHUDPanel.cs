using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainHUDPanel : MonoBehaviour
{
    [Serializable]
    public struct PanelInfo
    {
        [SerializeField] public KeyAsset key;
        [SerializeField] public string hotkey;
        [SerializeField] public UIPanel panel;
    }
    [SerializeField]
    List<PanelInfo> panels = new();

    Dictionary<string, UIPanel> keyToPanel = new();

    private void Start()
    {
        var uiSvc = Services.Get<IUIService>();
        foreach(var info in panels)
        {
            keyToPanel.Add(info.key, info.panel);
            uiSvc.RegisterHotkey(info.hotkey, info.key);
        }
        Game.Model.UI.CurrentOpenWindow.ValueChanged += OnCurrentWindowChanged;
    }

    void OnCurrentWindowChanged(string previousWindowKey, string windowKey)
    {
        if (!string.IsNullOrEmpty(previousWindowKey))
        {
            keyToPanel[previousWindowKey].SetShowing(false);
        }

        if (!string.IsNullOrEmpty(windowKey))
        {
            keyToPanel[windowKey].SetShowing(true);
        }
    }
}
