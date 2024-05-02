﻿using Components.Health;
using Creatures.Player;
using UnityEngine;

namespace Weapons
{
    public class Bullet : MonoBehaviour
    {
        private Rigidbody2D _rigidbody;

        private float _damage;
        private float _speed;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        public void Initialize(float damage, float speed, float ttl)
        {
            this._damage = damage;
            this._speed = speed;
            Destroy(gameObject, ttl);
        }

        private void Update()
        {
            _rigidbody.velocity = transform.right * _speed;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.isTrigger ||
                (collision.transform.parent != null && collision.transform.parent.TryGetComponent<PlayerController>(out PlayerController player)))
                return;

            if (collision.transform.parent != null &&
                collision.transform.parent.TryGetComponent(out HealthComponent health) &&
                health != PlayerController.Instance.GetComponent<HealthComponent>())
            {
                float damage = StatsModifier.GetModifiedDamage(_damage, DamageType.Bullet);
                health.ModifyHealth(-damage);
                MyGameManager.AddAmountDamage(damage);
            }
            Destroy(gameObject);
        }
    }
}
