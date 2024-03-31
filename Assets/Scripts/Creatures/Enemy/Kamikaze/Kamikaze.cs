using Components.Health;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Weapons.ThrownTrap;

namespace Creatures.Enemy
{
    public class Kamikaze : MonoBehaviour
    {
        [SerializeField] private GameObject _visual;
        [Space]
        [SerializeField] private float _damage;
        [SerializeField] private CircleCollider2D _attackRange;
        [SerializeField] private float _attackDelay;

        private Rigidbody2D _rigidbody;
        private KamikazeAnimations _animations;
        private SpawnComponents _spawner;

        private bool _isWakeUp;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _animations = _visual.GetComponent<KamikazeAnimations>();
            _spawner = GetComponent<SpawnComponents>();
        }

        private void Start()
        {
            _animations.OnWakeUpEnd += KamikazeAnimations_OnWakeUpEnd;
            _animations.OnAttackEnd += KamikazeAnimations_OnAttackEnd;
        }

        public void WakeUp()
        {
            if (_isWakeUp)
                return;

            _animations.WakeUp();
            Vector2 wakeUpPosition = new Vector2(transform.position.x, transform.position.y + 1);
            transform.position = Vector2.Lerp(transform.position, wakeUpPosition, 1f);
            _isWakeUp = true;
        }

        private void KamikazeAnimations_OnWakeUpEnd(object sender, EventArgs e)
        {
            _visual.GetComponent<CircleCollider2D>().isTrigger = false;
            Invoke(nameof(Attack), _attackDelay);
        }

        private void Attack()
        {
            _animations.Attack();
        }

        private void KamikazeAnimations_OnAttackEnd(object sender, EventArgs e)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(_attackRange.transform.position, _attackRange.radius);

            foreach (Collider2D collider in colliders.Where(collider =>
                     !collider.isTrigger && collider.GetComponentInParent<HealthComponent>() != null))
            {
                collider.GetComponentInParent<HealthComponent>().ModifyHealth(-_damage);
            }

            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            _spawner.Spawn();

            _animations.OnWakeUpEnd -= KamikazeAnimations_OnWakeUpEnd;
            _animations.OnAttackEnd -= KamikazeAnimations_OnAttackEnd;
        }
    }
}