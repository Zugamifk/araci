using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpriteAnimation
{
    public class AnimationEvents : MonoBehaviour
    {
        public GameObject Root;

        public void Destroy()
        {
            if (Application.isPlaying)
            {
                Destroy(Root);
            }
        }

        public void Disable()
        {
            if (Application.isPlaying)
            {
                Root.SetActive(false);
            }
        }
    }
}