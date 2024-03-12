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

        private float health;

        public EventHandler<OnValueChangeEventArgs> OnValueChange;
        public class OnValueChangeEventArgs : EventArgs
        {
            public float value;
            public float maxValue;
        }

        private void Start()
        {
            health = _maxHealth;
            LoadData(PlayerPrefsController.GetPlayerData());

            OnValueChange?.Invoke(this, new OnValueChangeEventArgs
            {
                value = health,
                maxValue = _maxHealth,
            });
        }

        private void LoadData(PlayerData data)
        {
            _maxHealth = data.MaxMana;
            health = data.Mana;
        }

        public void ModifyHealth(float changeValue)
        {            
            health = Mathf.Min(health + changeValue, _maxHealth);
            OnValueChange?.Invoke(this, new OnValueChangeEventArgs
            {
                value = health,
                maxValue = _maxHealth,
            });

            if (changeValue < 0)
                _onDamage?.Invoke();
            if (health <= 0)
                _onDie?.Invoke();
            if(changeValue > 0)
                _onHeal?.Invoke();
        }

        public (float health, float maxHealth) SaveData()
        {
            return (health, _maxHealth);
        }
    }
}