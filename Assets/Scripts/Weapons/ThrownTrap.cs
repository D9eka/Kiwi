using Components.Health;
using System.Linq;
using UnityEngine;
using UnityEngine.Animations;
using Weapons;

namespace Weapons
{
    public class ThrownTrap : MonoBehaviour
    {
        [SerializeField] private CircleCollider2D _attackRange;
        [Space]
        [SerializeField] private TrapDestroyType _mode;
        [SerializeField] private float _ttl;

        protected const string ATTACK_KEY = "attack";

        public enum TrapDestroyType
        {
            AfterAttack,
            TimeLimit
        }

        private Rigidbody2D _rigidbody;
        private Animator _animator;

        private float _damage;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
        }

        public void Initialize(float damage, float attackDelay, Vector2 force)
        { 
            _damage = damage;

            _rigidbody.velocity = force;

            InvokeRepeating(nameof(Attack), attackDelay, attackDelay);

            if (_mode == TrapDestroyType.TimeLimit)
            {
                Invoke(nameof(DestroyIt), _ttl);
            }

        }

        private void Attack()
        {
            _animator.SetTrigger(ATTACK_KEY);

            Collider2D[] colliders = Physics2D.OverlapCircleAll(_attackRange.transform.position, _attackRange.radius);

            float damage = _damage;
            if (_mode == TrapDestroyType.TimeLimit)
                damage += (_damage / 2) * colliders.Count(collider => collider.GetComponent<ThrownTrap>() != null);

            foreach (Collider2D collider in colliders.Where(collider => 
                     !collider.isTrigger && collider.GetComponentInParent<HealthComponent>() != null))
            {
                collider.GetComponentInParent<HealthComponent>().ModifyHealth(-damage);
            }
        }

        private void DestroyIt()
        {
            Destroy(gameObject);
        }
    }
}
