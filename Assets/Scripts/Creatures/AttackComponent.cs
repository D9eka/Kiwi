using Components.ColliderBased;
using Components.Health;
using Creatures.Player;
using System;
using System.Linq;
using UnityEngine;

namespace Creatures
{
    public class AttackComponent : MonoBehaviour
    {
        [SerializeField] private string _animationTrigger;
        [SerializeField] private float _damage;
        [SerializeField] private float _cooldownTime;
        [SerializeField] private AttackComponentType _attackType;
        [Space]
        [SerializeField] private ColliderTrigger _distance;
        [SerializeField] private Vector2 _initialPosition;

        public enum AttackComponentType
        {
            Simple,
            WithDistance,
            WithInitialPosition
        }

        private bool _cooldown;
        private bool _canAttackPlayer;

        public float Damage => _damage;

        public bool CanAttack
        {
            get => _canAttackPlayer && !_cooldown;
        }
        public bool Cooldown => _cooldown;

        private void Start()
        {
            switch (_attackType)
            {
                case AttackComponentType.Simple:
                    _canAttackPlayer = true;
                    break;
                case AttackComponentType.WithDistance:
                    _distance.OnPlayerEnterTrigger += Distance_OnPlayerEnterTrigger;
                    _distance.OnPlayerExitTrigger += Distance_OnPlayerExitTrigger;
                    break;
                case AttackComponentType.WithInitialPosition:
                    _canAttackPlayer = true;
                    break;
                default:
                    break;
            }
        }

        private void Distance_OnPlayerEnterTrigger(object sender, EventArgs e)
        {
            _canAttackPlayer = true;
        }

        private void Distance_OnPlayerExitTrigger(object sender, EventArgs e)
        {
            _canAttackPlayer = false;
        }

        public string Attack()
        {
            _cooldown = true;
            Invoke(nameof(RemoveCooldown), _cooldownTime);
            return _animationTrigger;
        }

        public HealthComponent[] OnAttack()
        {
            if (!_distance)
                return new HealthComponent[] { PlayerController.Instance.GetComponent<HealthComponent>() };

            Collider2D distanceCollider = _distance.GetComponent<Collider2D>();

            if (distanceCollider is CircleCollider2D circleCollider)
            {
                return Physics2D.OverlapCircleAll(circleCollider.transform.position, circleCollider.radius)
                            .Where(collider => !collider.isTrigger && collider.GetComponentInParent<HealthComponent>() != null)
                            .Select(collider => collider.GetComponentInParent<HealthComponent>()).ToArray();
            }
            else if (distanceCollider is BoxCollider2D boxCollider)
            {
                return Physics2D.OverlapBoxAll(boxCollider.transform.position, boxCollider.size, 0)
                            .Where(collider => !collider.isTrigger && collider.GetComponentInParent<HealthComponent>() != null)
                            .Select(collider => collider.GetComponentInParent<HealthComponent>()).ToArray();
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        private void RemoveCooldown()
        {
            _cooldown = false;
        }
    }
}
