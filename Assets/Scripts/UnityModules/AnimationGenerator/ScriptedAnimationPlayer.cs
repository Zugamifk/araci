using MeshGenerator;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

namespace AnimationGenerator
{
    public class ScriptedAnimationPlayer
    {
        public bool Loop;

        ScriptedAnimationData _data;
        Binding _binding;

        public delegate void Binding(float value);

        public ScriptedAnimationPlayer(ScriptedAnimationData data, Binding binding)
        {
            _data = data;
            _binding = binding;
        }

        public IEnumerator Play()
        {
            do
            {
                Evaluate(0);
                for (float t = 0; t < 1; t += Time.deltaTime)
                {
                    Evaluate(t);
                    yield return null;
                }
            }
            while (Loop);
        }

        public void Reset()
        {
            Evaluate(0);
        }

        public void Evaluate(float t)
        {
            var val = _data.Evaluate(t);
            _binding(val);
        }
    }
}
