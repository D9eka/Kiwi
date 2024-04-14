using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Components.UI
{
    public class ValueSlider : MonoBehaviour
    {
        [SerializeField] private string[] _values;
        [SerializeField] private ValueSliderInitialValue _initialValue;
        [Space]
        [SerializeField] private Button _previousButton;
        [SerializeField] private TextMeshProUGUI _value;
        [SerializeField] private Button _nextButton;

        public enum ValueSliderInitialValue
        {
            First,
            Last
        }

        private int _selectedValue;

        public string Value => _values[_selectedValue];

        public void SetValues(string[] values)
        {
            _selectedValue = 0;
            _values = values;
            Awake();
        }

        private void Awake()
        {
            switch (_initialValue)
            {
                case ValueSliderInitialValue.First:
                    _selectedValue = 0;
                    break;
                case ValueSliderInitialValue.Last:
                    _selectedValue = _values.Length - 1;
                    break;
                default:
                    break;
            }
            SetValue(_selectedValue);

            _previousButton.onClick.AddListener(() => SetValue(_selectedValue - 1));
            _nextButton.onClick.AddListener(() => SetValue(_selectedValue + 1));
        }

        private void SetValue(int valueIndex)
        {
            if (_selectedValue == valueIndex || valueIndex < 0 || valueIndex >= _values.Length)
                return;

            _selectedValue = valueIndex;
            _value.text = _values[_selectedValue];
        }
    }
}