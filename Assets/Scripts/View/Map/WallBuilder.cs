using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

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
    [SerializeField, Min(.1f)]
    float _sectionWidth = 1;
    [SerializeField]
    List<Section> _sections = new();
    [SerializeField]
    WallSection _prefab;
    [SerializeField]
    Transform _sectionRoot;

    void UpdateSections()
    {
        _sections.Clear();

        var a = transform.position;
        var b = _endpoint.position;
        var sections = _sectionRoot.GetComponentsInChildren<WallSection>();
        var pos = 0f;
        var distance = (b - a).magnitude;
        for (int i = 0; i < sections.Length; i++)
        {
            pos += _sectionWidth;
            if (pos < distance)
            {
                if (sections[i] == null)
                {
                    InstantiateSection();
                } else
                {
                    _sections.Add(new Section()
                    {
                        Instance = sections[i],
                        Type = sections[i].WallType
                    });
                }
            } else if(sections[i]!=null)
            {
                DestroyImmediate(sections[i].gameObject);
            }
        }

        while (pos < distance)
        {
            InstantiateSection();
            pos += _sectionWidth;
        }

        var flip = (a.x > b.x && a.y < b.y) || (a.x < b.x && a.y > b.y);
        var flipRotation = Quaternion.AngleAxis(180, Vector3.up);
        for (int i = 0; i < _sections.Count; i++)
        {
            var t = (float)i * _sectionWidth / distance;
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
