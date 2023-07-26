using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITransformService : IService
{
    void RegisterTransform(Guid id, Transform transform);
    Transform GetTransform(Guid id);
}
