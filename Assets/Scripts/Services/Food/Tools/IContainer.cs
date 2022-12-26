using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Food
{
    public interface IContainer
    {
        public ISet<IIngredient> Contents { get; set; }
    }
}