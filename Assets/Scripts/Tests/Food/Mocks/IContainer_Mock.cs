using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Food.Tests
{
    public class IContainer_Mock : Container
    {
        public override float Volume { get; }

        public IContainer_Mock(float volume)
        {
            Volume = volume;
        }
    }
}
