using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformService : ITransformService
{
    Dictionary<Guid, Transform> guidToTransformLookup = new Dictionary<Guid, Transform>();
    public Transform GetTransform(Guid id)
    {
        if(guidToTransformLookup.TryGetValue(id, out Transform transform))
        {
            return transform;
        }

        return default;
    }

    public void RegisterTransform(Guid id, Transform transform)
    {
        guidToTransformLookup.Add(id, transform);
    }
}
