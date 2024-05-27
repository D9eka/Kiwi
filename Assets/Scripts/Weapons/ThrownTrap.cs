using Components.Health;
using Creatures.Player;
using System.Linq;
using UnityEngine;
using static Weapons.WeaponSO;

namespace Weapons
{
    public class ThrownTrap : MonoBehaviour
    {
        [SerializeField] private WeaponSO _data;
        [Space]
        [SerializeField] private CircleCollider2D _attackRange;
        [SerializeField] private Vector2 _initialForce;

        private Rigidbody2D _rigidbody;
        private Animator _animator;

        private float _damage;
        private TrapDestroyType _destroyType;

        private const string ATTACK_KEY = "attack";

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
            _damage = _data.Damage;
            _destroyType = _data.DestroyType;

            _rigidbody.velocity = new Vector2(_initialForce.x * PlayerController.Instance.transform.localScale.x, _initialForce.y);

            InvokeRepeating(nameof(Attack), _data.AttackDelay, _data.AttackDelay);

            if (_destroyType == TrapDestroyType.TimeLimit)
            {
                Invoke(nameof(DestroyIt), _data.TTLSeconds);
            }
        }

        private void Attack()
        {
            _animator.SetTrigger(ATTACK_KEY);

            Collider2D[] colliders = Physics2D.OverlapCircleAll(_attackRange.transform.position, _attackRange.radius);

            float damage = StatsModifier.GetModifiedDamage(_damage, DamageType.Trap);
            if (_destroyType == TrapDestroyType.TimeLimit)
                damage += (_damage / 2) * colliders.Count(collider => collider.GetComponent<ThrownTrap>() != null);

            foreach (Collider2D collider in colliders.Where(collider =>
                     !collider.isTrigger && collider.GetComponentInParent<HealthComponent>() != null))
            {
                collider.GetComponentInParent<HealthComponent>().ModifyHealth(-damage);
                MyGameManager.AddAmountDamage(damage);
            }
            if (_data.ThrownTrapSound != null)
                SoundManager.Instance.PlaySound(_data.ThrownTrapSound);
        }

        private void DestroyIt()
        {
            Destroy(gameObject);
        }
    }
}
