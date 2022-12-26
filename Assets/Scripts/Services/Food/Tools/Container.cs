using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Food
{
    public abstract class Container : Tool
    {
        public abstract float Volume { get; }
        public ISet<Ingredient> Contents { get; set; } = new HashSet<Ingredient>();

        public float GetContentsVolume()
        {
            float volume = 0;
            foreach(var i in Contents)
            {
                volume += i.Volume;
            }
            return volume;
        }
    }
}