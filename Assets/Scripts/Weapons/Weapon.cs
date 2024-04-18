using System;
using UnityEngine;
using static Weapons.WeaponSO;
using Random = UnityEngine.Random;

namespace Weapons
{
    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField] protected WeaponSO _data;

        public WeaponSO Data => _data;

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
            switch (_data.DamageType)
            {
                case WeaponDamageType.Static:
                    _currentDamage = _data.Damage;
                    break;
                case WeaponDamageType.Random:
                    _currentDamage = Random.Range(_data.MinDamage, _data.MaxDamage);
                    break;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}