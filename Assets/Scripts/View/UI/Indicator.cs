using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Indicator : MonoBehaviour
{
    [SerializeField]
    Image _sprite;

    RectTransform _rectTransform;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        var target = Game.Model.Input.CurrentInteractable.Value;
        if(target!=Guid.Empty)
        {
            _sprite.enabled = true;
            UpdatePosition(target);
        }else
        {
            _sprite.enabled = false;
        }
    }

    void UpdatePosition(Guid id)
    {
        var targetObject = ViewLookup.Get(id);
        var target = targetObject.GetComponent<InteractableTarget>();
        var pos = Camera.main.WorldToViewportPoint(target.IndicatorPosition);
        _rectTransform.anchorMin = pos;
        _rectTransform.anchorMax = pos;
    }
}
