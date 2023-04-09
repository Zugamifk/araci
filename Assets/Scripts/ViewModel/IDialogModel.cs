using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDialogModel : IIdentifiable, IKeyHolder
{
    Guid SpeakerId { get; }
    string CurrentLine { get; }
}
