using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionTarget : MonoBehaviour
{
    [SerializeField]
    TargetType targetType;

    public bool IsType(TargetType actionTargetType)
    {
        return (targetType & actionTargetType) != 0;
    }
}
