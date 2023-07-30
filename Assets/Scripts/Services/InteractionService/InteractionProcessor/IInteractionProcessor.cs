using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractionProcessor
{
    void ProcessInteraction(Guid id);
}
