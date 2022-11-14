using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MeshGenerator;

public class MeshGeneratorUser : MonoBehaviour
{
    protected void AssignMesh<TMeshGenerator, TModel, TData>(MeshFilter meshFilter, TModel model)
        where TMeshGenerator : ModelMeshGenerator<TModel, TData>, new()
        where TModel : IIdentifiable
        where TData : IMeshGeneratorData
    {
        var gen = new TMeshGenerator();
        gen.Model = model;
        var res = gen.Generate();
        meshFilter.mesh = res.Mesh;
    }

    protected void AssignMesh<TMeshGenerator>(MeshFilter meshFilter, System.Action<TMeshGenerator> initialize = null)
        where TMeshGenerator : MeshGenerator.MeshGenerator, new()
    {
        var gen = new TMeshGenerator();
        initialize?.Invoke(gen);
        var res = gen.Generate();
        meshFilter.mesh = res.Mesh;
    }

    protected void AssignMesh<TMeshGenerator>(GeneratedSkinnedMeshRenderer renderer, System.Action<TMeshGenerator> initialize = null)
        where TMeshGenerator : MeshGenerator.MeshGenerator, new()
    {
        var gen = new TMeshGenerator();
        initialize?.Invoke(gen);
        var res = gen.Generate();
        renderer.ApplyMesh(res);
    }
}
