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

    Identifiable _id;

    private void Awake()
    {
        _id = GetComponent<Identifiable>();
        ViewLookup.Register(_id.Id, gameObject);
    }

    private void Start()
    {
        Game.Do(new RegisterShrine(_id.Id));
    }

    private void Update()
    {
        var model = Game.Model.Shrines.GetItem(_id.Id);
        if(model == null)
        {
            return;
        }

        _interactable.IsInteractable = model.HasBlessingAvailable;
        for (int i=0;i<_candles.Length;i++)
        {
            _candles[i].enabled = model.HasBlessingAvailable;
        }
    }
}
