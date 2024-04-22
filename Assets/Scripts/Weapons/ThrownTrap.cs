using Components.Health;
using System.Linq;
using UnityEngine;
using static Weapons.WeaponSO;

namespace Weapons
{
    public class ThrownTrap : MonoBehaviour
    {
        [SerializeField] private CircleCollider2D _attackRange;
		[SerializeField] protected AudioClip _sound;

        private Rigidbody2D _rigidbody;
        private Animator _animator;

        private float _damage;
        private TrapDestroyType _destroyType;

        protected const string ATTACK_KEY = "attack";

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
        }

        public void Initialize(float damage, float attackDelay, TrapDestroyType destroyType, float ttl, Vector2 force)
        { 
            _damage = damage;
            _destroyType = destroyType;

            _rigidbody.velocity = force;

            InvokeRepeating(nameof(Attack), attackDelay, attackDelay);

            if (_destroyType == TrapDestroyType.TimeLimit)
            {
                Invoke(nameof(DestroyIt), ttl);
            }

        }

        private void Attack()
        {
            _animator.SetTrigger(ATTACK_KEY);

            Collider2D[] colliders = Physics2D.OverlapCircleAll(_attackRange.transform.position, _attackRange.radius);

            float damage = _damage;
            if (_destroyType == TrapDestroyType.TimeLimit)
                damage += (_damage / 2) * colliders.Count(collider => collider.GetComponent<ThrownTrap>() != null);

            foreach (Collider2D collider in colliders.Where(collider => 
                     !collider.isTrigger && collider.GetComponentInParent<HealthComponent>() != null))
            {
                collider.GetComponentInParent<HealthComponent>().ModifyHealth(-damage);
            }
            SoundManager.Instance.PlaySound(_sound);
        }

        private void DestroyIt()
        {
            Destroy(gameObject);
        }
    }
}
