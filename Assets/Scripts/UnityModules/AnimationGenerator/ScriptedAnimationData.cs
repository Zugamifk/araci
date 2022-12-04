using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ScriptedAnimationData
{
    public AnimationCurve Curve;
    public float Duration = 1;
    public float Magnitude = 1;

    public float Evaluate(float t)
    {
        return Curve.Evaluate((t % Duration) / Duration) * Magnitude;
    }
}
