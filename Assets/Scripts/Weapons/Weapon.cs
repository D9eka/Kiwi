using UnityEngine;

namespace Weapons
{
    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField] protected float _attackDelay;
        [SerializeField] protected DamageType _mode;

        [SerializeField] private float _damage;

        [SerializeField] protected float _minDamage;
        [SerializeField] protected float _maxDamage;

        public enum DamageType
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
                case DamageType.Static:
                    _currentDamage = _damage; 
                    break;
                case DamageType.Random:
                    _currentDamage = Random.Range(_minDamage, _maxDamage);
                    break;
                default:
                    break;
            }
        }
    }
}