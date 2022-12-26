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
        TMP_Dropdown _foodOptionsDropdown;
        [SerializeField]
        TMP_InputField _weight;
        [SerializeField]
        TMP_InputField _volume;
        [SerializeField]
        TMP_InputField _heatTransferRate;
        [SerializeField]
        Slider _solidity;
        [SerializeField]
        TMP_InputField _minimumCookTemperature;
        [SerializeField]
        TMP_InputField _cookRate;
        [SerializeField]
        TMP_InputField _startTemperature;
        [SerializeField]
        RectTransform _heatProgress;
        [SerializeField]
        RectTransform _cookProgress;
        [SerializeField]
        TextMeshProUGUI _temperature;
        [SerializeField]
        TextMeshProUGUI _cookedPercent;
        [SerializeField]
        TMP_InputField _cookTemperature;

        float _startTemperatureValue;
        FoodModel _foodModel;
        Dictionary<string, IFoodData> _nameToDataLookup = new();

        private void Start()
        {
            Clear();
            LoadOptions();
            FillInfoFields();
        }

        void Clear()
        {
            _foodOptionsDropdown.options.Clear();
            _foodOptionsDropdown.value = 0;
            _weight.text = string.Empty;
            _volume.text = string.Empty;
            _heatTransferRate.text = string.Empty;
            _solidity.value = 0;
            _minimumCookTemperature.text = string.Empty;
            _cookRate.text = string.Empty;
            _startTemperature.text = string.Empty;
            _cookTemperature.text = string.Empty;

            ResetCooking();
        }

        void ResetCooking()
        {
            UpdateProgress(_heatProgress, 0);
            UpdateProgress(_cookProgress, 0);

            _temperature.text = $"Temperature: 0 C";
            _cookedPercent.text = $"Cooking Progress: 0%";

            _foodModel = null;
        }

        void LoadOptions()
        {
            var dataCollection = DataService.GetData<FoodDataCollection>();
            var options = new List<TMP_Dropdown.OptionData>();
            foreach(var data in dataCollection.AllData)
            {
                options.Add(new TMP_Dropdown.OptionData(data.Name));
                _nameToDataLookup.Add(data.Name, data);
            }

            _foodOptionsDropdown.options.Clear();
            _foodOptionsDropdown.AddOptions(options);
            _foodOptionsDropdown.value = 0;
        }

        private void Update()
        {
            if(_foodModel!=null)
            {
                UpdateFoodModel();
            }
        }

        public void OnClickedStart()
        {
            CreateFoodModel();
        }

        public void OnSelectedDropdownItem()
        {
            ResetCooking();
            FillInfoFields();
        }

        void CreateFoodModel()
        {
            _startTemperatureValue = float.Parse(_startTemperature.text);
            _foodModel = new FoodModel()
            {
                Weight = float.Parse(_weight.text),
                Volume = float.Parse(_volume.text),
                CookTemperature = float.Parse(_minimumCookTemperature.text),
                HeatTransferRate = float.Parse(_heatTransferRate.text),
                SolidPercent = _solidity.value,
                CookRate = float.Parse(_cookRate.text),
                Temperature = float.Parse(_startTemperature.text)
            };
        }

        void FillInfoFields()
        {
            var option = _foodOptionsDropdown.options[_foodOptionsDropdown.value];
            var data = _nameToDataLookup[option.text];
            _weight.text = data.Weight.ToString();
            _volume.text = data.Volume.ToString();
            _heatTransferRate.text = data.HeatTransferRate.ToString();
            _solidity.value = data.SolidPercent;
            _minimumCookTemperature.text = data.CookTemperature.ToString();
            _cookRate.text = data.CookRate.ToString();
            _startTemperature.text = "0";
            _cookTemperature.text = "0";
        }

        void UpdateFoodModel()
        {
            var service = Services.Get<IFoodService>();
            service.Heat(_foodModel, float.Parse(_cookTemperature.text), Time.deltaTime);

            UpdateProgress(_heatProgress, Mathf.InverseLerp(_startTemperatureValue, _foodModel.CookTemperature, _foodModel.Temperature));
            UpdateProgress(_cookProgress, _foodModel.CookedPercent);

            _temperature.text = $"Temperature: {_foodModel.Temperature:0.0} C";
            _cookedPercent.text = $"Cooking Progress: {Mathf.RoundToInt(100* _foodModel.CookedPercent)}%";
        }

        void UpdateProgress(RectTransform bar, float percent)
        {
            var anchor = bar.anchorMax;
            anchor.x = percent;
            bar.anchorMax = anchor;
            bar.sizeDelta = Vector2.zero;
        }
    }
}
