using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthMarker : MonoBehaviour
{
    [SerializeField]
    Image _view;

    private void OnEnable()
    {
        _view.enabled = true;
    }

    private void OnDisable()
    {
        _view.enabled = false;
    }
}
