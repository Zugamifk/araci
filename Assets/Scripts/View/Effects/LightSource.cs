using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSource : MonoBehaviour
{
    [SerializeField]
    Animator _sourceAnimator; 
    [SerializeField]
    SpriteRenderer _spriteRenderer;

    private void OnEnable()
    {
        SetEnabled(true);
    }

    private void OnDisable()
    {
        SetEnabled(false);
    }

    void SetEnabled(bool enabled)
    {
        _sourceAnimator.enabled = enabled;
        _spriteRenderer.enabled = enabled;
    }
}
