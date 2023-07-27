using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackedTransform : MonoBehaviour
{
    void Start()
    {
        var id = GetComponent<Identifiable>();
        id.IdChanged += OnIdChanged;
    }

    void OnIdChanged(Guid id)
    {
        if(id == Guid.NewGuid())
        {
            return;
        }

        var tfService = Services.Get<ITransformService>();
        tfService.RegisterTransform(id, transform);
    }
}
