using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MeshGenerator.Wireframes;

namespace MeshGenerator
{
    public abstract class MeshGeneratorWithData<TData> : MeshGenerator
        where TData : IMeshGeneratorData
    {
        static TData _data;
        public TData Data
        {
            get
            {
                if (_data == null)
                {
                    _data = LoadData();
                }
                return _data;
            }
        }
        protected abstract TData LoadData();

    }
}
