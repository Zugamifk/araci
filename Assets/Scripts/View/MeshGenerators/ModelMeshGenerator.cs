using MeshGenerator;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ModelMeshGenerator<TModel, TData> : MeshGeneratorWithData<TData>
    where TModel : IIdentifiable
    where TData : IMeshGeneratorData
{
    public TModel Model { get; set; }
}
