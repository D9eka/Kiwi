using Components.ColliderBased;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Creatures.Enemy
{
    public class EnemyController : Creature
    {
        [Header("Checkers")]
        [SerializeField] private CheckCircleOverlap _attackRange;

        private const string HIT_KEY = "hit";
        private const string ATTACK_KEY = "attack";

        protected override void Awake()
        {
            base.Awake();
        }

        protected override void Update()
        {
            base.Update();
        }

        public void Stop()
        {
            direction = Vector2.zero;
            _rigidbody.velocity = Vector2.zero;
        }

        public void TakeDamage()
        {
            //_animator.SetTrigger(HIT_KEY);
        }

        public void Attack()
        {
            //_animator.SetTrigger(ATTACK_KEY);
            OnDoAttack();
        }

        public void OnDoAttack()
        {
            _attackRange.Check();
        }

        public override void Die()
        {
            _rigidbody.velocity = Vector2.zero;
            base.Die();
        }
    }
}