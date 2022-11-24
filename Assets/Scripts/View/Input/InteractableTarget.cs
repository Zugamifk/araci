using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableTarget : MonoBehaviour
{
    [SerializeField]
    Transform _indicatorPosition;

    public Vector3 IndicatorPosition => _indicatorPosition.position;
    public bool IsInteractable;
}
