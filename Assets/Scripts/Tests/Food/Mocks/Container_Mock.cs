using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Food.Tests
{
    public class Container_Mock : Container
    {
        public override float Volume { get; set; } = 4000;

        public Container_Mock() { }

        public Container_Mock(float volume)
        {
            Volume = volume;
        }
    }
}
