using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    [SerializeField]
    GameObject m_Root;

    public void Destroy()
    {
        Destroy(m_Root);
    }
}
