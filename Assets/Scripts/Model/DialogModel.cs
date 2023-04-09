using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogModel : IDialogModel
{
    public Guid Id { get; } = Guid.NewGuid();
    public string Key { get; set; }
    public Guid SpeakerId { get; set; }
    public string CurrentLine { get; set; }
    public int CurrentLineIndex { get; set; }
}
