using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;
using UnityEditor.Animations;

public static class AnimatorStateMachineExtensions
{
    static object[] _getStatePosition_args = new object[1];
    static readonly MethodInfo _getStatePosition = typeof(AnimatorStateMachine)
        .GetMethod("GetStatePosition", BindingFlags.NonPublic | BindingFlags.Instance, null, CallingConventions.Any, new Type[] { typeof(AnimatorState) }, null);

    public static Vector3 GetStatePosition(this AnimatorStateMachine sm, AnimatorState state)
    {
        _getStatePosition_args[0] = state;
        return (Vector3)_getStatePosition.Invoke(sm, _getStatePosition_args);
    }

    static object[] _setStatePosition_args = new object[2];
    static readonly MethodInfo _setStatePosition = typeof(AnimatorStateMachine)
        .GetMethod("SetStatePosition", BindingFlags.NonPublic | BindingFlags.Instance, null, CallingConventions.Any, new Type[] { typeof(AnimatorState), typeof(Vector3) }, null);

    public static void SetStatePosition(this AnimatorStateMachine sm, AnimatorState state, Vector3 position)
    {
        _setStatePosition_args[0] = state;
        _setStatePosition_args[1] = position;
        _setStatePosition.Invoke(sm, _setStatePosition_args);
    }
}
