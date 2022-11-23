using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedAnimationEffect : MonoBehaviour
{
    [SerializeField]
    Animator _animator;
    [SerializeField]
    float _delay;

    public void Play()
    {
        transform.SetParent(null);
        gameObject.SetActive(true);
        StartCoroutine(Play_Coroutine());
    }

    IEnumerator Play_Coroutine()
    {
        var delay = Random.value * _delay;
        yield return new WaitForSeconds(delay);
        PlayEffect();
    }

    void PlayEffect()
    {
        _animator.enabled = true;
    }
}
