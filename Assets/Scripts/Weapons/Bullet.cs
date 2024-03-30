﻿using Components.Health;
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
            if (collision.isTrigger)
                return;

            if (collision.transform.parent.TryGetComponent(out HealthComponent health))
                health.ModifyHealth(-_damage);
            Destroy(gameObject);
        }
    }
}
