using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Weapons
{
    public abstract class Weapon : MonoBehaviour
    {
        public WeaponSO WeaponSO { get; set; }
        [SerializeField] protected float _attackDelay;
        [SerializeField] protected WeaponDamageType _mode;

        [SerializeField] private float _damage;

        [SerializeField] protected float _minDamage;
        [SerializeField] protected float _maxDamage;
        [SerializeField] protected AudioClip _sound;

        public float AttackSpeed => 1 / _attackDelay;

        public string DamageInfo
        {
            get
            {
                return _mode switch
                {
                    WeaponDamageType.Static => _damage.ToString(),
                    WeaponDamageType.Random => _minDamage + "-" + _maxDamage,
                    _ => "-"
                };
            }
        }


        public enum WeaponDamageType
        {
            Static,
            Random
        }

        protected Animator _animator;

        protected float _timeBetweenAttacks;
        protected float _currentDamage;

        protected const string ATTACK_KEY = "attack";

        protected virtual void Update()
        {
            _timeBetweenAttacks += Time.deltaTime;
        }


        public virtual void Attack()
        {
            switch (_mode)
            {
                case WeaponDamageType.Static:
                    _currentDamage = _damage;
                    break;
                case WeaponDamageType.Random:
                    _currentDamage = Random.Range(_minDamage, _maxDamage);
                    break;
                default:
                    break;
            }
        }
    }
}