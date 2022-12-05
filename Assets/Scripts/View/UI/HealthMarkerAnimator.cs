using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthMarkerAnimator : MonoBehaviour
{
    [SerializeField]
    HealthMarker[] _healthMarkers;
    [SerializeField]
    ScriptedAnimationData _movementAnimation;
    [SerializeField, Range(0, 180)]
    float _maxAngle;

    private void Update()
    {
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

        if (pc.Movement.Speed > 0)
        {
            angle += _movementAnimation.Evaluate(Time.time);
        }

        marker.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
