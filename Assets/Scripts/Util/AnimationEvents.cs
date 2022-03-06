using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    public GameObject Root;

    public void Destroy()
    {
        Destroy(Root);
    }
}
