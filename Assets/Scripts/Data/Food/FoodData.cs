using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Food
{
    [CreateAssetMenu(menuName = "Data/Food")]
    public class FoodData : ScriptableObject, IKeyHolder
    {
        [field: SerializeField]
        public string Key
        {
            get; set;
        }

        [field: SerializeField]
        public string Name
        {
            get; set;
        }

        [field: SerializeField]
        [field: TextArea]
        public string Description
        {
            get; set;
        }

        [field: SerializeField]
        public float Weight
        {
            get; set;
        }

        [field: SerializeField]
        public float Volume
        {
            get; set;
        }
    }
}