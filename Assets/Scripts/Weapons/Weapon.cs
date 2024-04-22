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
        public string Label { get; protected set; }
        public EventHandler OnChangeLabel;

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
            _currentDamage = _data.DamageType switch
            {
                WeaponDamageType.Static => _data.Damage,
                WeaponDamageType.Random => Random.Range(_data.MinDamage, _data.MaxDamage),
                _ => throw new NotImplementedException(),
            };
        }
    }
}