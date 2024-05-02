using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Components.UI
{
    public class ValueSlider : MonoBehaviour
    {
        [SerializeField] private List<string> _values;
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

        public EventHandler<string> OnChangeValue;

        public void SetValues(List<string> values)
        {
            if (values == null || values.Count == 0)
                return;

            _selectedValue = 0;
            _values = values;
            SetValueIndex();
        }

        private void Awake()
        {
            SetValueIndex();
            _previousButton.onClick.AddListener(() => ModifyValueIndex(-1));
            _nextButton.onClick.AddListener(() => ModifyValueIndex(1));
        }

        public void ModifyValueIndex(int modifier)
        {
            SetValueIndex(_selectedValue + modifier);
        }

        private void SetValueIndex()
        {
            switch (_initialValue)
            {
                case ValueSliderInitialValue.First:
                    _selectedValue = 0;
                    break;
                case ValueSliderInitialValue.Last:
                    _selectedValue = _values.Count - 1;
                    break;
                default:
                    break;
            }
            SetValueIndex(_selectedValue);
        }

        public void SetValueIndex(int valueIndex)
        {
            if (_selectedValue == valueIndex || valueIndex < 0 || valueIndex >= _values.Count)
                return;

            _selectedValue = valueIndex;
            _value.text = _values[_selectedValue];
            OnChangeValue?.Invoke(this, _values[_selectedValue]);
        }
    }
}