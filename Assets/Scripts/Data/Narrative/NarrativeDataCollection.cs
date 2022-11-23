using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Narrative
{
    public class NarrativeDataCollection : ScriptableObject, IRegisteredData
    {
        [SerializeField]
        List<NarrativeData> _data;

        Dictionary<string, NarrativeData> _dataDictionary = new();

        void OnEnable()
        {
            foreach (var narrative in _data)
            {
                _dataDictionary[narrative.Key] = narrative;
            }
        }

        public NarrativeData GetData(string key)
        {
            return _dataDictionary[key];
        }
    }
}