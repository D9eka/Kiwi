using Sections;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Components.Oxygen
{
    public class OxygenComponent : MonoBehaviour
    {
        [SerializeField] private float _maxOxygen;
        [SerializeField] private float _decreaseValue = 1f;
        [SerializeField] private float _decreaseTime = 1f;
        [Space][SerializeField] private UnityEvent _onRestore;
        [SerializeField] private UnityEvent _onEnd;

        private float _oxygen;

        public EventHandler<OnValueChangeEventArgs> OnValueChange;

        public class OnValueChangeEventArgs : EventArgs
        {
            public float value;
            public float maxValue;
        }

        private void Start()
        {
            Restore();
            if (!Section.Instance.TypeSO.IsOxygenWasting)
                return;
            StartCoroutine(Decrease());
        }

        public void Restore()
        {
            _oxygen = _maxOxygen;

            _onRestore?.Invoke();

            OnValueChange?.Invoke(this, new OnValueChangeEventArgs
            {
                value = _oxygen,
                maxValue = _maxOxygen,
            });
        }

        private IEnumerator Decrease()
        {
            while (_oxygen > 0f)
            {
                _oxygen -= _decreaseValue;
                OnValueChange?.Invoke(this, new OnValueChangeEventArgs
                {
                    value = _oxygen,
                    maxValue = _maxOxygen,
                });
                yield return new WaitForSeconds(_decreaseTime);
            }

            _onEnd?.Invoke();
        }

        public void ModifyOxygen(float changeValue)
        {
            _oxygen = Mathf.Min(_oxygen + changeValue, _maxOxygen);

            if (changeValue < 0)
            {
                _onRestore?.Invoke();
                _onEnd?.Invoke();
            }
        }

        public void ModifyOxygenPercent(float changeValuePercent)
        {
            ModifyOxygen(_maxOxygen * changeValuePercent);
        }

        public void ChangeOxygenStats(float addingValue)
        {
            _maxOxygen += addingValue;
            if (addingValue > 0)
            {
                _oxygen += addingValue;
            }
            OnValueChange?.Invoke(this, new OnValueChangeEventArgs
            {
                value = _oxygen,
                maxValue = _maxOxygen,
            });
        }
    }
}