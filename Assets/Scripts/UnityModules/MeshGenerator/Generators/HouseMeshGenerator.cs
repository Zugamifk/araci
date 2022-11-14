using MeshGenerator.Wireframes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeshGenerator
{
    [MeshGenerator("House")]
    public class HouseMeshGenerator : MeshGenerator
    {
        public class GeometryData : ScriptableObject
        {
            public float Rotation;

            public Vector2 FloorDimensions;

            public float BaseExtents = 1;
            public float Height = 3;

            public float RoofPeak = 2;
            public float EavesLength = 1;
            public float WindowHeight = 1;

            [Serializable]
            public class DoorData
            {
                public float Position = .5f;
                public Vector2 Dimensions = Vector2.one;
                public int Wall = 0;
            }

            [Serializable]
            public class WindowData
            {
                public float Position;
                public Vector2 Dimensions = Vector2.one;
            }

            [Serializable]
            public class WallData
            {
                public List<WindowData> Windows = new();
            }
            public List<WallData> Walls = new();

            public DoorData Door;

            public static GeometryData Instance;
            private void OnEnable()
            {
                if (Walls.Count != 4)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        Walls.Add(new());
                    }
                }

                Instance = this;
            }
        }

        class WallSectionPointData
        {
            public List<Vector3> BoundPoints = new();
            public List<Vector3> Intervals = new();
        }

        // wireframe points
        List<Vector3> _basePoints = new();
        List<WallSectionPointData> _walls = new();
        List<Vector3> _roofPoints = new();
        List<Vector3> _atticWallPoints = new();

        public GeometryData Data => _data;
        public Wireframe Wireframe => _wireframe;

        static GeometryData _data => GeometryData.Instance;

        Wireframe _wireframe;
        Vector3[] _wallCorners;

        static HouseMeshGenerator()
        {
            if (_data == null)
            {
                ScriptableObject.CreateInstance<GeometryData>();
                _data.hideFlags = HideFlags.HideAndDontSave;
            }
        }

        protected override void BuildMesh(MeshBuilder builder)
        {
            //base
            builder.AddQuad(_basePoints[0], _basePoints[1], _basePoints[2], _basePoints[3]);

            //walls
            builder.AddTriangle(_atticWallPoints[0], _atticWallPoints[1], _atticWallPoints[2]);
            builder.AddTriangle(_atticWallPoints[3], _atticWallPoints[4], _atticWallPoints[5]);

            //roof
            builder.AddQuad(_roofPoints[0], _roofPoints[1], _roofPoints[4], _roofPoints[5]);
            builder.AddQuad(_roofPoints[2], _roofPoints[1], _roofPoints[4], _roofPoints[3]);
            builder.AddQuad(_roofPoints[0], _roofPoints[5], _roofPoints[4], _roofPoints[1]);
            builder.AddQuad(_roofPoints[2], _roofPoints[3], _roofPoints[4], _roofPoints[1]);
        }

        public void BuildWireframe()
        {
            _wireframe = new();
            var d = Data;

            // base
            float fx = d.FloorDimensions.x / 2 + d.BaseExtents;
            float fy = d.FloorDimensions.y / 2 + d.BaseExtents;

            _basePoints.Clear();
            _basePoints.Add(new Vector3(-fx, 0, -fy));
            _basePoints.Add(new Vector3(-fx, 0, fy));
            _basePoints.Add(new Vector3(fx, 0, fy));
            _basePoints.Add(new Vector3(fx, 0, -fy));

            _wireframe.Connect(_basePoints[0], _basePoints[1]);
            _wireframe.Connect(_basePoints[1], _basePoints[2]);
            _wireframe.Connect(_basePoints[2], _basePoints[3]);
            _wireframe.Connect(_basePoints[3], _basePoints[0]);

            // walls
            var h = new Vector3(0, d.Height, 0);
            float bx = d.FloorDimensions.x / 2;
            float by = d.FloorDimensions.y / 2;
            var w1 = new Vector3(-bx, 0, by);
            var w0 = new Vector3(-bx, 0, -by);
            var w2 = new Vector3(bx, 0, by);
            var w3 = new Vector3(bx, 0, -by);
            _wallCorners = new[] { w0, w1, w2, w3 };

            _wireframe.Connect(w0, w1);
            _wireframe.Connect(w1, w2);
            _wireframe.Connect(w2, w3);
            _wireframe.Connect(w3, w0);

            var w4 = w0 + Vector3.up * d.Height;
            var w5 = Vector3.Lerp(w0, w1, .5f) + Vector3.up * (d.Height + d.RoofPeak);
            var w6 = w1 + Vector3.up * d.Height;
            var w7 = w2 + Vector3.up * d.Height;
            var w8 = Vector3.Lerp(w2, w3, .5f) + Vector3.up * (d.Height + d.RoofPeak);
            var w9 = w3 + Vector3.up * d.Height;

            _wireframe.Connect(w0, w4);
            _wireframe.Connect(w1, w6);
            _wireframe.Connect(w2, w7);
            _wireframe.Connect(w3, w9);

            _wireframe.Connect(w4, w5);
            _wireframe.Connect(w5, w6);
            _wireframe.Connect(w6, w7);
            _wireframe.Connect(w7, w8);
            _wireframe.Connect(w8, w9);
            _wireframe.Connect(w9, w4);

            _atticWallPoints = new() { w5, w4, w6, w8, w7, w9 };

            

            // roof
            Vector3 rd = (w2 - w1).normalized;
            Vector3 rdl = (w4 - w5).normalized;
            Vector3 rdr = (w6 - w5).normalized;

            var r0 = w4 - rd * d.EavesLength + rdl * d.EavesLength;
            var r1 = w5 - rd * d.EavesLength;
            var r2 = w6 - rd * d.EavesLength + rdr * d.EavesLength;
            var r3 = w7 + rd * d.EavesLength + rdr * d.EavesLength;
            var r4 = w8 + rd * d.EavesLength;
            var r5 = w9 + rd * d.EavesLength + rdl * d.EavesLength;

            _wireframe.Connect(r0, r1);
            _wireframe.Connect(r1, r2);
            _wireframe.Connect(r2, r3);
            _wireframe.Connect(r3, r4);
            _wireframe.Connect(r4, r5);
            _wireframe.Connect(r5, r0);
            _wireframe.Connect(r1, r4);

            _roofPoints = new() { r0, r1, r2, r3, r4, r5 };

            Vector3 wp0, wp1;

            // windows
            for (int i = 0; i < 4; i++)
            {
                var w = d.Walls[i];
                wp0 = _wallCorners[i];
                wp1 = _wallCorners[(i + 1) % 4];
                var wd = (wp1 - wp0).normalized;

                for (int j = 0; j < w.Windows.Count; j++)
                {
                    var window = w.Windows[j];
                    var ww0 = Vector3.Lerp(wp1 - wd * window.Dimensions.x, wp0, window.Position) + Vector3.up * d.WindowHeight;
                    var ww1 = ww0 + Vector3.up * window.Dimensions.y;
                    var ww2 = ww0 + Vector3.up * window.Dimensions.y + wd * window.Dimensions.x;
                    var ww3 = ww0 + wd * window.Dimensions.x;

                    _wireframe.Connect(ww0, ww1);
                    _wireframe.Connect(ww1, ww2);
                    _wireframe.Connect(ww2, ww3);
                    _wireframe.Connect(ww3, ww0);
                }
            }

            var di = d.Door.Wall;
            wp0 = _wallCorners[di];
            wp1 = _wallCorners[(di + 1) % 4];
            var dir = (wp1 - wp0).normalized;
            var d0 = Vector3.Lerp(wp1 - dir * d.Door.Dimensions.x, wp0, d.Door.Position);
            var d1 = d0 + Vector3.up * d.Door.Dimensions.y;
            var d2 = d1 + dir * d.Door.Dimensions.x;
            var d3 = d0 + dir * d.Door.Dimensions.x;

            _wireframe.Connect(d0, d1);
            _wireframe.Connect(d1, d2);
            _wireframe.Connect(d2, d3);
        }
    }
}
