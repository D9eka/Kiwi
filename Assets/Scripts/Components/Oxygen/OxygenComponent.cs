using Creatures.Player;
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
        [Space]
        [SerializeField] private UnityEvent _onRestore;
        [SerializeField] private UnityEvent _onEnd;

        private float _oxygen;
        private float Oxygen
        {
            get
            {
                return _oxygen;
            }
            set
            {
                _oxygen = value;
                OnValueChange?.Invoke(this, new OnValueChangeEventArgs
                {
                    value = _oxygen,
                    maxValue = _maxOxygen,
                });
                if (_isPlayer)
                    PlayerPrefsController.SetVector2(SAVE_KEY, new Vector2(_oxygen, _maxOxygen));
            }
        }
        private bool _isPlayer;

        public const string SAVE_KEY = "PlayerOxygen";

        public EventHandler<OnValueChangeEventArgs> OnValueChange;

        public class OnValueChangeEventArgs : EventArgs
        {
            public float value;
            public float maxValue;
        }

        private void Awake()
        {
            if (TryGetComponent<PlayerController>(out PlayerController playerController))
            {
                SetPlayerOxygen();
            }
            else
            {
                Oxygen = _maxOxygen;
            }
        }

        private void Start()
        {
            if ((Section.Instance != null && !Section.Instance.TypeSO.IsOxygenWasting) ||
                (SectionTutorial.Instance != null && !SectionTutorial.Instance.IsOxygenWasting))
                return;
            StartCoroutine(Decrease());
        }

        private void SetPlayerOxygen()
        {
            Vector2 savedData = PlayerPrefsController.GetVector2(SAVE_KEY);
            if (savedData != Vector2.zero)
            {
                _maxOxygen = savedData.y;
                Oxygen = savedData.x;
            }
            else
            {
                Oxygen = _maxOxygen;
            }
            _isPlayer = true;
        }

        public void Restore()
        {
            Oxygen = _maxOxygen;
        }

        private IEnumerator Decrease()
        {
            while (Oxygen > 0f)
            {
                ModifyOxygen(-_decreaseValue);
                yield return new WaitForSeconds(_decreaseTime);
            }
            _onEnd?.Invoke();
        }

        public void ModifyOxygen(float changeValue)
        {
            if (_isPlayer && !PlayerController.Instance.Active)
                return;

            Oxygen = Mathf.Min(Oxygen + changeValue, _maxOxygen);

            if (Oxygen <= 0)
            {
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
                Oxygen += addingValue;
            }
            else
            {
                OnValueChange?.Invoke(this, new OnValueChangeEventArgs
                {
                    value = _oxygen,
                    maxValue = _maxOxygen,
                });
            }
        }
    }
}