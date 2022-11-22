using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    GameObject _deathEffect;

    private void OnDestroy()
    {
        _deathEffect.transform.SetParent(transform.parent);
        _deathEffect.SetActive(true);
        _deathEffect.GetComponent<Animator>().enabled = true;
    }
}
