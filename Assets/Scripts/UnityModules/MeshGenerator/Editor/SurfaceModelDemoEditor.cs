using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace MeshGenerator.Surfaces.Editor
{
    [CustomEditor(typeof(SurfaceModelDemo))]
    public class SurfaceModelDemoEditor : UnityEditor.Editor
    {
        ProcessStepper _stepper = new();

        private void OnEnable()
        {
            _stepper.Builder = new((target as SurfaceModelDemo).Model);
            _stepper.AddStep(new AddVertexStep(new Vector3(0, 0, 0)));
            _stepper.AddStep(new AddVertexStep(new Vector3(0, 1, 0)));
            _stepper.AddStep(new ConnectVerticesStep(0, 1));
            _stepper.AddStep(new AddVertexStep(new Vector3(1,1,0)));
            _stepper.AddStep(new ConnectVerticesStep(1, 2));
            _stepper.AddStep(new ConnectVerticesStep(0, 2));
            _stepper.AddStep(new AddFaceStep(0, 1, 2));
            _stepper.AddStep(new AddVertexStep(new Vector3(1, 0, 0)));
            _stepper.AddSteps(ConnectVerticesStep.InSubsteps(0, 3));
            //_stepper.AddStep(new ConnectVerticesStep(0, 3));
            _stepper.AddStep(new ConnectVerticesStep(3, 2));
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GUILayout.Label("Next: "+_stepper.CurrentStepLabel);

            using (new EditorGUILayout.HorizontalScope()) {
                if (GUILayout.Button("<<"))
                {
                    _stepper.StepBack();
                    SceneView.RepaintAll();
                }

                GUILayout.Label($"Step {_stepper.CurrentStep}");

                if (GUILayout.Button(">>"))
                {
                    _stepper.StepForward();
                    SceneView.RepaintAll();
                }
            }

            if(GUILayout.Button("Clear"))
            {
                _stepper.Reset();
                SceneView.RepaintAll();
            }
        }

        void OnSceneGUI()
        {
            var smd = (SurfaceModelDemo)target;
            var model = smd.Model;

            if (model == null) return;

            Handles.matrix = smd.transform.localToWorldMatrix;

            var fwd = SceneView.currentDrawingSceneView.camera.transform.forward;

            Handles.color = Color.blue;
            foreach (var v in model.Vertices)
            {
                Handles.DrawSolidDisc(v.Position, fwd, .1f);
                if(v.HalfEdge!=null)
                {
                    var v1 = v.Position;
                    var v2 = v.HalfEdge.Twin?.Vertex.Position ?? v1 + Vector3.up;
                    var dir = (v2 - v1).normalized;
                    var n = Vector3.Cross(dir, fwd);
                    var hepos = v.Position + dir * smd.HalfEdgeShorten + n * smd.HalfEdgeDistance;
                    HandleX.DrawArrow(v.Position, hepos, fwd, .02f);
                }
                if (smd.ShowVertexLabels)
                {
                    Handles.Label(v.Position, v.Label);
                }
            }

            Handles.color = Color.green;
            foreach (var e in model.Edges)
            {
                var v1 = e.HalfEdge.Vertex;
                var v2 = e.HalfEdge.Twin.Vertex;
                if (smd.ShowEdgeLabels)
                {
                    Handles.Label(Vector3.Lerp(v1.Position, v2.Position, .5f), e.ToString());
                }
                Handles.DrawLine(v1.Position, v2.Position);
            }

            foreach (var h in model.HalfEdges)
            {
                var v1 = h.Vertex.Position;
                var v2 = h.Twin?.Vertex.Position ?? v1 + Vector3.up;
                var dir = (v2 - v1).normalized;
                var n = Vector3.Cross(dir, fwd);

                var p1 = v1 + dir * smd.HalfEdgeShorten + n * smd.HalfEdgeDistance;
                var p2 = v2 - dir * smd.HalfEdgeShorten + n * smd.HalfEdgeDistance;

                Handles.color = Color.yellow;
                HandleX.DrawArrow(p1, p2, fwd, .02f);

                if (smd.ShowHalfEdgeLabels)
                {
                    Handles.Label(Vector3.Lerp(p1, p2, .25f), h.ToString());
                }

                if (h.Next != null)
                {
                    var v3 = h.Next?.EndVertex?.Position ?? Vector3.zero;
                    var dir2 = (v3 - v2).normalized;
                    var n2 = Vector3.Cross(dir2, fwd);
                    var p3 = v2 + dir2 * smd.HalfEdgeShorten + n2 * smd.HalfEdgeDistance;

                    Handles.color = Color.red;
                    HandleX.DrawArrow(p2, p3, fwd, .02f);
                }
            }

            foreach(var f in model.Faces)
            {
                var center = f.Centre();
                Handles.color = Color.magenta;
                foreach(var edge in f.HalfEdge.Loop())
                {
                    var p0 = edge.Vertex.Position;
                    var f0 = p0 + (center - p0).normalized * smd.FaceInset;
                    var p1 = edge.Next.Vertex.Position;
                    var f1 = p1 + (center - p1).normalized * smd.FaceInset;
                    HandleX.DrawArrow(Vector3.Lerp(p0, p1, .5f), Vector3.Lerp(f0, f1, .5f), fwd, 0.02f);
                    Handles.DrawLine(f0, f1);
                }
            }
        }
    }
}
