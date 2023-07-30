using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionService : IInteractionService
{
    Dictionary<Type, IInteractionProcessor> modelTypeToInteractionProcessor = new();
    Dictionary<Guid, IInteractionProcessor> guidToInteractionProcessor = new();
    public InteractionService()
    {
        PopulateInteractionProcessors();
    }

    void PopulateInteractionProcessors()
    {
        modelTypeToInteractionProcessor[typeof(IShrineModel)] = new ShrineInteractionProcessor();
        modelTypeToInteractionProcessor[typeof(IHarvestableModel)] = new HarvestableActionProcessor();
    }

    public void RegisterInteractable<TInteractable>(Guid id)
    {
        var type = typeof(TInteractable);
        if (!modelTypeToInteractionProcessor.TryGetValue(type, out IInteractionProcessor processor))
        {
            throw new ArgumentException($"No processor for interactble of type {type}!");
        }

        guidToInteractionProcessor[id] = processor;
    }

    public void ProcessInteraction(Guid id)
    {
        if (!guidToInteractionProcessor.TryGetValue(id, out IInteractionProcessor processor))
        {
            throw new ArgumentException($"No processor registered for {id}!");
        }
        processor.ProcessInteraction(id);
    }
}
