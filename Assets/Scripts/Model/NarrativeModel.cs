using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarrativeModel : IIdentifiable
{
    public Guid Id { get; set; }
    public string NarrativeKey { get; set; }
    public Guid CurrentStateId { get; set; }
}
