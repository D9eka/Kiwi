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
        [SerializeField] private UnityEvent _onDie;

        private float _health;
        private float Health
        {
            get
            {
                return _health;
            }
            set
            {
                if (_isPlayer && value > _health)
                    MyGameManager.DamageHealed += Mathf.RoundToInt(value - _health);
                _health = value;
                OnValueChange?.Invoke(this, new OnValueChangeEventArgs
                {
                    value = _health,
                    maxValue = _maxHealth,
                });
                if (_isPlayer)
                    PlayerPrefsController.SetVector2(SAVE_KEY, new Vector2(_health, _maxHealth));
            }
        }
        private bool _isPlayer;

        public const string SAVE_KEY = "PlayerHealth";

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
                SetPlayerHealth();
            }
            else
            {
                Health = _maxHealth;
            }
        }

        private void SetPlayerHealth()
        {
            Vector2 savedData = PlayerPrefsController.GetVector2(SAVE_KEY);
            if (savedData != Vector2.zero)
            {
                _maxHealth = savedData.y;
                Health = savedData.x;
            }
            else
            {
                Health = _maxHealth;
            }
            _isPlayer = true;
        }

        public void ModifyHealth(float changeValue)
        {
            if (_isPlayer && !PlayerController.Instance.Active)
                return;
            if (_isPlayer && changeValue < 0 && ChipManager.Instance.TryUseShieldChip())
                return;

            Health = Mathf.Clamp(Health + changeValue, 0, _maxHealth);

            if (changeValue < 0)
            {
                if (_isPlayer)
                {
                    SoundManager.Instance?.PlaySound(SoundManager.Instance._takeDamageSound);
                }
                _onDamage?.Invoke();
            }
            if (Health <= 0)
            {
                if (_isPlayer && ChipManager.Instance.TryUseRevivalChip(out float healthPercent))
                {
                    ModifyHealth(_maxHealth * healthPercent);
                    return;
                }
                _onDie?.Invoke();
            }
            if (changeValue > 0)
                _onHeal?.Invoke();
        }

        public void ModifyHealthPercent(float changeValuePercent)
        {
            ModifyHealth(_maxHealth * changeValuePercent);
        }

        public void ChangeHealthStats(float addingValue)
        {
            _maxHealth += addingValue;
            if (addingValue > 0)
            {
                Health += addingValue;
            }
            else
            {
                OnValueChange?.Invoke(this, new OnValueChangeEventArgs
                {
                    value = _health,
                    maxValue = _maxHealth,
                });
            }
        }
    }
}