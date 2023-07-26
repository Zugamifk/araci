using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class IMovementModelExtensions
{
    const float APPROXIMATE_POSITION_ERROR = 0.1f;
    public static bool IsApproximateAtPosition(this IPositionModel model, Vector2 position)
    {
        return (model.Position.Value - position).sqrMagnitude < APPROXIMATE_POSITION_ERROR;
    }
}
