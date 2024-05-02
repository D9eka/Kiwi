using Components.Health;
using Creatures.Player;
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
            if (_timeBetweenAttacks < _data.AttackDelay)
                return;

            _timeBetweenAttacks = 0;
        }

        public void OnAttack()
        {
            base.Attack();
            Collider2D[] colliders = Physics2D.OverlapCircleAll(_attackRange.transform.position, _attackRange.radius);
            foreach (Collider2D collider in colliders)
            {
                if (!collider.isTrigger && collider.transform.parent.TryGetComponent(out HealthComponent health) &&
                    health != PlayerController.Instance.GetComponent<HealthComponent>())
                {
                    float damage = StatsModifier.GetModifiedDamage(_currentDamage, DamageType.Melee);
                    health.ModifyHealth(-damage);
                    MyGameManager.AddAmountDamage(damage);
                }
            }
        }
    }
}