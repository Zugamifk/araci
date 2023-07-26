using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackedTransform : MonoBehaviour
{
    void Start()
    {
        var id = GetComponent<IObservable<Guid>>();
        id.ValueChanged += OnIdChanged;
    }

    void OnIdChanged(Guid _, Guid id)
    {
        if(id == Guid.NewGuid())
        {
            return;
        }

        var tfService = Services.Get<ITransformService>();
        tfService.RegisterTransform(id, transform);
    }
}
