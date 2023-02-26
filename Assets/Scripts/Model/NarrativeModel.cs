using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarrativeModel : IIdentifiable
{
    public Guid Id { get; set; }
    public string NarrativeKey { get; set; }
    public int CurrentActionIndex { get; set; } = -1;
}
