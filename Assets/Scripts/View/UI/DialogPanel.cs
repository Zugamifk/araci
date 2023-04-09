using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class DialogPanel : MonoBehaviour
{
    [SerializeField]
    CanvasGroup rootCanvasGroup;
    [SerializeField]
    TextMeshProUGUI speakerNameText;
    [SerializeField]
    TextMeshProUGUI dialogText;

    string currentString = string.Empty;
    Guid dialogId = Guid.Empty;

    private void Update()
    {
        var currentDialog = Game.Model.Dialog;
        if (currentDialog != null && currentDialog.Id != dialogId)
        {
            ShowPanel();
        } else if(currentDialog == null)
        {
            HidePanel();
            return;
        }
        
        if (currentDialog.CurrentLine != currentString)
        {
            UpdateText();
        }
    }

    void ShowPanel()
    {
        dialogId = Game.Model.Dialog.Id;
        currentString = String.Empty;
        SetShowing(true);
    }

    void HidePanel()
    {
        SetShowing(false);
        currentString = String.Empty;
        dialogId = Guid.Empty;
    }

    void SetShowing(bool isShowing)
    {
        rootCanvasGroup.alpha = isShowing ? 1 : 0;
        rootCanvasGroup.blocksRaycasts = isShowing;
        rootCanvasGroup.interactable = isShowing;
    }

    void UpdateText()
    {
        currentString = Game.Model.Dialog.CurrentLine;
        var speaker = Game.Model.Characters.GetItem(Game.Model.Dialog.SpeakerId);
        speakerNameText.text = speaker.DisplayName;
        dialogText.text = currentString;
    }
}
