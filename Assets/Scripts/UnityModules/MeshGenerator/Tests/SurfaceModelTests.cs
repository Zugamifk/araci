using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using MeshGenerator.Surfaces;

namespace MeshGenerator.Tests
{
    public class SurfaceModelTests
    {
        #region New
        [Test]
        public void New_IsEmpty()
        {
            var model = new SurfaceModel();

            Assert.That(model.Edges, Is.Empty);
            Assert.That(model.Faces, Is.Empty);
            Assert.That(model.HalfEdges, Is.Empty);
            Assert.That(model.Vertices, Is.Empty);
        }
        #endregion

        #region IsEmpty
        [Test]
        public void IsEmpty_TrueIfEmpty()
        {
            var model = new SurfaceModel();

            Assert.That(model.IsEmpty);
        }

        [Test]
        public void IsEmpty_FalseIfNotEmpty()
        {
            var model = new SurfaceModel();
            model.Vertices.Add(new Vertex());

            Assert.That(model.IsEmpty, Is.False);
        }
        #endregion

        #region New SurfaceModelBuilder
        [Test]
        public void NewBuild_EmptyModel()
        {
            var model = new SurfaceModel();
            var builder = new SurfaceModelBuilder(model);
            Assert.That(model.IsEmpty);
        }
        #endregion

        #region AddPoint()
        [Test]
        public void AddPoint_AddsVertex()
        {
            var model = new SurfaceModel();
            var builder = new SurfaceModelBuilder(model);

            builder.AddPoint(Vector3.zero);

            Assert.That(model.Vertices.Count, Is.EqualTo(1));
        }
        #endregion

        #region ConnectPoints()
        [Test]
        public void ConnectPoints_CreatesEdge()
        {
            var model = new SurfaceModel();
            var builder = new SurfaceModelBuilder(model);

            var v0 = builder.AddPoint(Vector3.zero);
            var v1 = builder.AddPoint(Vector3.up);
            builder.ConnectVertices(v0, v1);

            Assert.That(model.Edges.Count, Is.EqualTo(1));
        }

        [Test]
        public void ConnectPoints_CreatesHalfEdges()
        {
            var model = new SurfaceModel();
            var builder = new SurfaceModelBuilder(model);

            var v0 = builder.AddPoint(Vector3.zero);
            var v1 = builder.AddPoint(Vector3.up);
            builder.ConnectVertices(v0, v1);

            var edge = model.Edges[0];
            var h1 = edge.HalfEdge;
            var h2 = h1.Twin;

            Assert.That(model.HalfEdges.Count, Is.EqualTo(2));
            Assert.That(h1, Is.Not.Null);
            Assert.That(h2, Is.Not.Null);
            Assert.That(model.HalfEdges.Contains(h1));
            Assert.That(model.HalfEdges.Contains(h2));
        }

        [Test]
        public void ConnectPoints_CreatedHalfEdgesAssignedToEdge()
        {
            var model = new SurfaceModel();
            var builder = new SurfaceModelBuilder(model);

            var v0 = builder.AddPoint(Vector3.zero);
            var v1 = builder.AddPoint(Vector3.up);
            builder.ConnectVertices(v0, v1);

            var edge = model.Edges[0];
            var h1 = edge.HalfEdge;
            var h2 = h1.Twin;

            Assert.That(h1.Edge, Is.EqualTo(edge));
            Assert.That(h2.Edge, Is.EqualTo(edge));
        }

        [Test]
        public void ConnectPoints_CreatedHalfEdgesAreTwins()
        {
            var model = new SurfaceModel();
            var builder = new SurfaceModelBuilder(model);

            var v0 = builder.AddPoint(Vector3.zero);
            var v1 = builder.AddPoint(Vector3.up);
            builder.ConnectVertices(v0, v1);

            var edge = model.Edges[0];
            var h1 = edge.HalfEdge;
            var h2 = h1.Twin;

            Assert.That(h1.Twin, Is.EqualTo(h2));
            Assert.That(h2.Twin, Is.EqualTo(h1));
        }

        [Test]
        public void ConnectPoints_AssignsHalfEdgeToOrphanVertex()
        {
            var model = new SurfaceModel();
            var builder = new SurfaceModelBuilder(model);

            var v0 = builder.AddPoint(Vector3.zero);
            var v1 = builder.AddPoint(Vector3.up);
            builder.ConnectVertices(v0, v1);

            Assert.That(v0.HalfEdge, Is.Not.Null);
            Assert.That(v1.HalfEdge, Is.Not.Null);
        }

        [Test]
        public void ConnectPoints_ThrowOutOfRangeException_IfNoPoints()
        {
            var model = new SurfaceModel();
            var builder = new SurfaceModelBuilder(model);

            Assert.That(()=> builder.ConnectVertices(0, 1), 
                Throws.InstanceOf<System.ArgumentOutOfRangeException>());
        }

        [Test]
        public void ConnectPoints_AssignsVertexToHalfEdges()
        {
            var model = new SurfaceModel();
            var builder = new SurfaceModelBuilder(model);

            var v0 = builder.AddPoint(Vector3.zero);
            var v1 = builder.AddPoint(Vector3.up);
            builder.ConnectVertices(v0, v1);

            var h1 = model.HalfEdges[0];
            var h2 = model.HalfEdges[1];

            Assert.That(h1.Vertex, Is.Not.Null);
            Assert.That(h2.Vertex, Is.Not.Null);
        }

        [Test]
        public void ConnectPoints_OrphanPoints_NextIsTwin()
        {
            var model = new SurfaceModel();
            var builder = new SurfaceModelBuilder(model);

            var v0 = builder.AddPoint(Vector3.zero);
            var v1 = builder.AddPoint(Vector3.up);
            builder.ConnectVertices(v0, v1);

            var h1 = model.HalfEdges[0];
            var h2 = model.HalfEdges[1];

            Assert.That(h1.Next, Is.EqualTo(h2));
            Assert.That(h2.Next, Is.EqualTo(h1));
        }

        [Test]
        public void ConnectPoints_FirstIsFrom()
        {
            var model = new SurfaceModel();
            var builder = new SurfaceModelBuilder(model);

            var v1 = builder.AddPoint(Vector3.zero);
            var v2 = builder.AddPoint(Vector3.up);
            var e = builder.ConnectVertices(v1, v2);

            Assert.That(e.HalfEdge.StartVertex, Is.EqualTo(v1));
        }

        [Test]
        public void ConnectPoints_SecondIsTo()
        {
            var model = new SurfaceModel();
            var builder = new SurfaceModelBuilder(model);

            var v1 = builder.AddPoint(Vector3.zero);
            var v2 = builder.AddPoint(Vector3.up);
            var e = builder.ConnectVertices(v1, v2);

            Assert.That(e.HalfEdge.EndVertex, Is.EqualTo(v2));
        }

        [Test]
        public void ConnectPoints_ExistingEdge_ConnectsEdges()
        {
            var model = new SurfaceModel();
            var builder = new SurfaceModelBuilder(model);

            var v1 = builder.AddPoint(Vector3.zero);
            var v2 = builder.AddPoint(Vector3.up);
            var e1 = builder.ConnectVertices(v1, v2);

            var v3 = builder.AddPoint(Vector3.up);
            var e2 = builder.ConnectVertices(v2, v3);

            Assert.That(e1.IsConnectedTo(e2));
            Assert.That(e2.IsConnectedTo(e1));
        }

        [Test]
        public void ConnectPoints_ExistingEdge_HalfEdgeNextConnected()
        {
            var model = new SurfaceModel();
            var builder = new SurfaceModelBuilder(model);

            var v1 = builder.AddPoint(Vector3.zero);
            var v2 = builder.AddPoint(Vector3.up);
            var e1 = builder.ConnectVertices(v1, v2);

            var v3 = builder.AddPoint(Vector3.up);
            var e2 = builder.ConnectVertices(v2, v3);

            Assert.That(e1.HalfEdge.Next, Is.EqualTo(e2.HalfEdge));
        }

        [Test]
        public void ConnectPoints_NewHalfEdge_ConnectedToOutside()
        {
            var model = new SurfaceModel();
            var builder = new SurfaceModelBuilder(model);

            var v1 = builder.AddPoint(Vector3.zero);
            var v2 = builder.AddPoint(Vector3.up);
            var e = builder.ConnectVertices(v1, v2);

            Assert.That(e.HalfEdge.Face, Is.EqualTo(Face.Outside));
            Assert.That(e.HalfEdge.Twin.Face, Is.EqualTo(Face.Outside));
        }

        [Test]
        public void ConnectPoints_VertexEdges_ContainsNewEdge()
        {
            var model = new SurfaceModel();
            var builder = new SurfaceModelBuilder(model);

            var v1 = builder.AddPoint(Vector3.zero);
            var v2 = builder.AddPoint(Vector3.up);
            var e1 = builder.ConnectVertices(v1, v2);

            var v3 = builder.AddPoint(Vector3.up);
            var e2 = builder.ConnectVertices(v2, v3);

            Assert.That(v2.Edges(), Contains.Item(e1));
            Assert.That(v2.Edges(), Contains.Item(e2));
        }

        [Test]
        public void ConnectPoints_CloseLoop_CreatesEdgeLoop()
        {
            var model = new SurfaceModel();
            var builder = new SurfaceModelBuilder(model);

            var v1 = builder.AddPoint(Vector3.zero);
            var v2 = builder.AddPoint(Vector3.up);
            var v3 = builder.AddPoint(Vector3.right);

            var e = builder.ConnectVertices(v1, v2);
            builder.ConnectVertices(v2, v3);
            builder.ConnectVertices(v3, v1);

            var start = e.HalfEdge;
            var next = start;
            HashSet<HalfEdge> visited = new();
            var looped = false;
            do
            {
                visited.Add(next);
                next = next.Next;

                if (next == start)
                {
                    looped = true;
                    break;
                }
            } while (!visited.Contains(next));

            Assert.That(looped, Is.True);
        }
        #endregion

        #region Vertex.Edges
        [Test]
        public void Vertex_Edges_ReturnsConnectedEdges()
        {
            var v0 = new Vertex() { Label = "v0", Position = Vector3.zero };
            var v1 = new Vertex() { Label = "v1", Position = Vector3.up };
            var v2 = new Vertex() { Label = "v2", Position = Vector3.right };

            var e1 = new Edge();
            var h1 = new HalfEdge() { Label = "h1" };
            v0.HalfEdge = h1;
            e1.HalfEdge = h1;
            h1.Edge = e1;
            h1.Vertex = v0;
            var h2 = new HalfEdge() { Label = "h2" };
            h2.Edge = e1;
            h1.Twin = h2;
            h2.Twin = h1;
            h1.Next = h2;
            h2.Vertex = v1;
            var e2 = new Edge();
            var h3 = new HalfEdge() { Label = "h3" };
            e2.HalfEdge = h3;
            h3.Edge = e2;
            h2.Next = h3;
            h3.Vertex = v0;
            var h4 = new HalfEdge() { Label = "h4" };
            h4.Edge = e2;
            h3.Twin = h4;
            h4.Twin = h3;
            h3.Next = h4;
            h4.Next = h1;
            h4.Vertex = v2;

            Assert.That(v0.Edges().Contains(e1));
            Assert.That(v0.Edges().Contains(e2));
        }
        #endregion

        #region CreateFace()
        [Test]
        public void CreateFace_DisconnectedEdge_ThrowInvalidOperationException()
        {
            var model = new SurfaceModel();
            var builder = new SurfaceModelBuilder(model);

            var v1 = builder.AddPoint(Vector3.zero);
            var v2 = builder.AddPoint(Vector3.up);
            var e = builder.ConnectVertices(v1, v2);

            //Assert.Throws<System.InvalidOperationException>(() => builder.CreateFace(e.HalfEdge));
        }

        [Test]
        public void CreateFace_AllEdgesShareFace()
        {

        }
        #endregion
    }
}
