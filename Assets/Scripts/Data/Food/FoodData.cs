using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Food
{
    [CreateAssetMenu(menuName = "Data/Food")]
    public class FoodData : ScriptableObject, IKeyHolder, IFoodData
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
        public float Weight
        {
            get; set;
        }

        [field: SerializeField]
        public float Volume
        {
            get; set;
        }

        [field: SerializeField]
        public float HeatTransferRate
        {
            get; set;
        }

        [field: SerializeField]
        public float SolidPercent
        {
            get; set;
        }

        [field: SerializeField]
        public float CookTemperature
        {
            get; set;
        }

        [field: SerializeField]
        public float CookRate
        {
            get; set;
        }
    }
}