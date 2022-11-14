using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeshGenerator
{
    [AttributeUsage(AttributeTargets.Class)]
    public class MeshGeneratorAttribute : Attribute
    {
        public string Key;
        public MeshGeneratorAttribute(string key) => Key = key;
    }
}
