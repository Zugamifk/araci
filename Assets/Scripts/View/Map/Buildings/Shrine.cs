using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shrine : MonoBehaviour
{
    [SerializeField]
    LightSource[] candles;
    [SerializeField]
    InteractableTarget interactable;

    private void Start()
    {
        var id = GetComponent<IIdentifiable>().Id;
        Game.Do(new RegisterShrine(id, transform.position, OnShrineRegistered));
    }

    void OnShrineRegistered(IShrineModel shrine)
    {
        shrine.HasBlessingAvailable.ValueChanged += OnHasBlessingAvailableChanged;
    }

    void OnHasBlessingAvailableChanged(bool _, bool value)
    {
        interactable.IsInteractable = value;
        for (int i = 0; i < candles.Length; i++)
        {
            candles[i].enabled = value;
        }
    }
}
