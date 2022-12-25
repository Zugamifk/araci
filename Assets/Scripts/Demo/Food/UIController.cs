using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Food;

namespace Demo.Food
{
    public class UIController : MonoBehaviour
    {
        [SerializeField]
        Dropdown _foodOptionsDropdown;
        [SerializeField]
        TMP_InputField _weight;
        [SerializeField]
        TMP_InputField _volume;
        [SerializeField]
        TMP_InputField _heatTransferRate;
        [SerializeField]
        Slider _solidity;
        [SerializeField]
        TMP_InputField _cookTemperature;
        [SerializeField]
        TMP_InputField _cookRate;
        [SerializeField]
        TMP_InputField _startTemperature;
        [SerializeField]
        Image _heatProgress;
        [SerializeField]
        Image _cookProgress;
        [SerializeField]
        TextMeshProUGUI _temperature;
        [SerializeField]
        TextMeshProUGUI _cookedPercent;

        FoodModel _foodModel;

        private void Start()
        {
            Clear();
        }

        void Clear()
        {
            _foodOptionsDropdown.value = 0;
            _weight.text = string.Empty;
            _volume.text = string.Empty;
            _heatTransferRate.text = string.Empty;
            _solidity.value = 0;
            _cookTemperature.text = string.Empty;
            _cookRate.text = string.Empty;
            _startTemperature.text = string.Empty;
        }

        private void Update()
        {
            if(_foodModel!=null)
            {
                UpdateFoodModel();
            }
        }

        void OnClickedStart()
        {

        }

        void UpdateFoodModel()
        {
            var service = Services.Get<IFoodService>();
            service.Heat(_foodModel, float.Parse(_temperature.text), Time.deltaTime);
        }
    }
}
