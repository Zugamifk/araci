using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthMarker : MonoBehaviour
{
    [SerializeField]
    Image _view;

    public void UpdateState(bool enabled)
    {
        _view.enabled = enabled;
    }
}
