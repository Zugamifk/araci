using MeshGenerator.Wireframes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeshGenerator.Editor
{
    public abstract class WireframeGenerator<TData> 
    {
        public void Generate(Wireframe wireframe, TData data)
        {
            BuildWireframe(wireframe, data);
        }
        
        protected abstract void BuildWireframe(Wireframe wireframe, TData data);
    }
}
