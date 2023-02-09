using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBuilder : MonoBehaviour
{
    [Serializable]
    public struct Section
    {
        public WallSection Instance;
        public WallSection.Type Type;
    }

    [SerializeField, CallMethodButton("UpdateSections", "Generate Walls")]
    Transform _endpoint;
    [SerializeField, Min(.1f), CallMethodButton("ResetWalls", "Reset Walls")]
    float _sectionWidth = 1;
    [SerializeField]
    List<Section> _sections = new();
    [SerializeField]
    WallSection _prefab;
    [SerializeField]
    Transform _sectionRoot;

    void UpdateSections()
    {
        var sections = _sectionRoot.GetComponentsInChildren<WallSection>();
        foreach (var s in sections)
        {
            DestroyImmediate(s.gameObject);
        }
        _sections.Clear();

        var a = transform.position;
        var b = _endpoint.position;
        var pos = a.y < b.y ? _sectionWidth : 0f;
        var distance = (b - a).magnitude;

        while (pos < distance)
        {
            InstantiateSection();
            pos += _sectionWidth;
        }

        var flip = (a.x > b.x && a.y < b.y) || (a.x < b.x && a.y > b.y);
        var flipRotation = Quaternion.AngleAxis(180, Vector3.up);
        var offset = a.y < b.y ? 1 : 0;
        for (int i = 0; i < _sections.Count; i++)
        {
            var t = (float)(i+offset) * _sectionWidth / distance;
            var tf = _sections[i].Instance.transform;
            tf.position = Vector3.Lerp(a, b, t);
            if (flip)
            {
                tf.localRotation = flipRotation;
            } else
            {
                tf.localRotation = Quaternion.identity;
            }
        }
    }

    void InstantiateSection()
    {
        var section = Instantiate(_prefab);
        section.transform.parent = _sectionRoot;
        _sections.Add(new Section()
        {
            Instance = section,
            Type = section.WallType
        });
    }

    private void OnDrawGizmos()
    {
        if (_endpoint == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, _endpoint.position);
    }
}
