using MeshGenerator.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ModelWireframeGenerator<TModel, TData> : WireframeGenerator<TData>
    where TModel : new()
{
    public TModel Model { get; set; } = new();
}
