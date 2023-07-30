using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractionService : IService
{
    void RegisterInteractable<TInteractable>(Guid id);
    void ProcessInteraction(Guid id);
}
