using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shrine : MonoBehaviour
{
    [SerializeField]
    LightSource[] _candles;
    [SerializeField]
    InteractableTarget _interactable;

    private void Start()
    {
        var id = GetComponent<IIdentifiable>().Id;
        Game.Do(new RegisterShrine(id, OnShrineRegistered));
    }

    void OnShrineRegistered(IShrineModel shrine)
    {
        shrine.HasBlessingAvailable.ValueChanged += OnHasBlessingAvailableChanged;
    }

    void OnHasBlessingAvailableChanged(bool _, bool value)
    {
        _interactable.IsInteractable = value;
        for (int i = 0; i < _candles.Length; i++)
        {
            _candles[i].enabled = value;
        }
    }
}
