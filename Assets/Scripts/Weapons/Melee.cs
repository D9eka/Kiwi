using Components.Health;
using UnityEngine;

namespace Weapons
{
    public class Melee : Weapon
    {
        [SerializeField] private CircleCollider2D _attackRange;
        public float AttackRange => _attackRange.radius;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public override void Attack()
        {
            if (_timeBetweenAttacks < _attackDelay)
                return;

            _timeBetweenAttacks = 0;
            _animator.SetTrigger(ATTACK_KEY);
            SoundManager.Instance.PlaySound(_sound);
        }

        public void OnAttack()
        {
            base.Attack();

            Collider2D[] colliders = Physics2D.OverlapCircleAll(_attackRange.transform.position, _attackRange.radius);
            foreach (Collider2D collider in colliders)
            {
                if (!collider.isTrigger && !collider.CompareTag("Player") &&
                    collider.transform.parent.TryGetComponent(out HealthComponent health))
                {
                    health.ModifyHealth(-_currentDamage);
                }
            }
        }
    }
}