using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthMarkerAnimator : MonoBehaviour
{
    [SerializeField]
    HealthMarker[] _healthMarkers;
    [SerializeField]
    AnimationCurve _movementCurve;
    [SerializeField]
    float _movementStrength;
    [SerializeField, Range(0, 180)]
    float _maxAngle;

    float _animParam = 0;

    private void Update()
    {
        _animParam = (_animParam + Time.deltaTime) % 1;

        foreach (var h in _healthMarkers)
        {
            if (!h.enabled)
            {
                break;
            }

            UpdateMarker(h);
        }
    }

    void UpdateMarker(HealthMarker marker)
    {
        float angle = 0;
        var pc = Game.Model.PlayerCharacter;
        if (pc == null) return;

        if (pc.Movement.DesiredMove.sqrMagnitude > 0)
        {
            angle += _movementCurve.Evaluate(_animParam) * _movementStrength;
        }

        marker.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
