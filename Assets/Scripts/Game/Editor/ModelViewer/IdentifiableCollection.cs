using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdentifiableCollection
{
    public string DisplayName;
    public Dictionary<Guid, Identifiable> Identifiables = new Dictionary<Guid, Identifiable>();
}
