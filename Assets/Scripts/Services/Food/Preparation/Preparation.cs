using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Food
{
    public class Preparation
    {
        public string Name;
        public List<PreparationStep> Steps = new();
        public PreparationContext Context = new();
    }
}