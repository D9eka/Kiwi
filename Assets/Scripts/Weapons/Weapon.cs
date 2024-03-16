using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Weapons
{
    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField] protected float _damage;
        [SerializeField] protected float _attackDelay;

        protected Animator _animator;

        protected float timeBetweenAttacks;

        protected const string ATTACK_KEY = "attack";

        protected virtual void Update()
        {
            timeBetweenAttacks += Time.deltaTime;
        }

        public abstract void Attack();
    }
}