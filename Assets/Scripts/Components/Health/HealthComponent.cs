using Creatures.Player;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace Components.Health
{
    public class HealthComponent : MonoBehaviour
    {
        [SerializeField] private float _maxHealth;
        [SerializeField] private UnityEvent _onHeal;
        [SerializeField] private UnityEvent _onDamage;
        public UnityEvent _onDie;

        private float _health;

        public EventHandler<OnValueChangeEventArgs> OnValueChange;

        public class OnValueChangeEventArgs : EventArgs
        {
            public float value;
            public float maxValue;
        }

        private void Start()
        {
            _health = _maxHealth;

            OnValueChange?.Invoke(this, new OnValueChangeEventArgs
            {
                value = _health,
                maxValue = _maxHealth,
            });
            if (TryGetComponent(out PlayerController playerController))
            {
                //_onDamage.AddListener(() => SoundManager.Instance.PlaySound(SoundManager.Instance._takeDamageSound));
            }
        }

        public void ModifyHealth(float changeValue)
        {
            _health = Mathf.Min(_health + changeValue, _maxHealth);
            OnValueChange?.Invoke(this, new OnValueChangeEventArgs
            {
                value = _health,
                maxValue = _maxHealth,
            });

            if (changeValue < 0)
                _onDamage?.Invoke();
            if (_health <= 0)
                _onDie?.Invoke();
            if (changeValue > 0)
                _onHeal?.Invoke();

            Debug.Log($"{gameObject.name}: {_health} ({changeValue})");
        }

        public void ModifyHealthPercent(float changeValuePercent)
        {
            ModifyHealth(_maxHealth * changeValuePercent);
        }

        public (float health, float maxHealth) SaveData()
        {
            return (_health, _maxHealth);
        }

        public void ChangeHealthStats(float addingValue)
        {
            _maxHealth += addingValue;
            if (addingValue > 0)
            {
                _health += addingValue;
            }

            OnValueChange?.Invoke(this, new OnValueChangeEventArgs
            {
                value = _health,
                maxValue = _maxHealth,
            });
        }
    }
}