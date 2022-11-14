using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace MeshGenerator.Surfaces
{
    [ExecuteInEditMode]
    public class SurfaceModelDemo : MonoBehaviour
    {
        [SerializeField]
        public float HalfEdgeDistance = .1f;
        public float HalfEdgeShorten = .1f;
        public float FaceInset = .25f;
        public bool ShowVertexLabels;
        public bool ShowEdgeLabels;
        public bool ShowHalfEdgeLabels;

        public SurfaceModel Model = new();
        void OnEnable()
        {
            //Problem();
            //CubeFromMesh();

            //var va = new SurfaceModelValidator();
            //va.Validate(Model);

            //var bevel = new BevelOperation(new(Model));
            //bevel.BevelEdges(.2f, Model.Edges[0], Model.Edges[1]);
        }

        void CubeFromMesh()
        {
            var cb = new CubeMeshGenerator();
            var res = cb.Generate();
            var m = res.Mesh;
            var smb = new MeshToSurfaceModelBuilder();
            Model = smb.ConvertMesh(m);
        }

        void Problem()
        {
            Model = new SurfaceModel();
            var builder = new SurfaceModelBuilder(Model);
            //var v0 = builder.AddPoint(Vector3.zero);
            //var v1 = builder.AddPoint(Vector3.up);
            //var v2 = builder.AddPoint(new Vector3(1, 1, 0));
            //var v3 = builder.AddPoint(Vector3.right);
            //var v4 = builder.AddPoint(Vector3.right * 2);
            //builder.CreateFace(v0, v1, v2);
            ////builder.ConnectPoints(v0, v3);
            ////builder.CreateFace(2, 4, 3);
            //builder.CreateFace(0, 2, 3);
        }

        void TwoByTwo()
        {
            Model = new SurfaceModel();
            var builder = new SurfaceModelBuilder(Model);
            var v0 = builder.AddPoint(Vector3.zero);
            var v1 = builder.AddPoint(Vector3.up);
            var v2 = builder.AddPoint(new Vector3(1, 1, 0));
            var v3 = builder.AddPoint(Vector3.right);
            var e1 = builder.ConnectVertices(v0, v1);
            builder.CreateFace(v0, v1, v2, v3);
            var v4 = builder.AddPoint(new Vector3(0, 2, 0));
            var v5 = builder.AddPoint(new Vector3(1, 2, 0));
            var e2 = builder.ConnectVertices(v1, v4);
            builder.CreateFace(v1, v4, v5, v2);
            var v6 = builder.AddPoint(new Vector3(2, 0, 0));
            var v7 = builder.AddPoint(new Vector3(2, 1, 0));
            var v8 = builder.AddPoint(new Vector3(2, 2, 0));
            builder.ConnectVertices(v2, v7);
            builder.ConnectVertices(v5, v8);
            builder.ConnectVertices(v6, v7);
            builder.ConnectVertices(v7, v8);
            builder.ConnectVertices(v3, v6);
        }
    }
}
