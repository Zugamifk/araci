using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace MeshGenerator.Editor
{
    [AttributeUsage(AttributeTargets.Class)]
    public class MeshGeneratorEditorAttribute : Attribute
    {
        public Type GeneratorType;
        public MeshGeneratorEditorAttribute(Type generatorType)
        {
            GeneratorType = generatorType;
        }
    }
}
