using Components.Health;
using Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Weapons
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _ttl;

        private Rigidbody2D _rigidbody;

        private float damage;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();

            Destroy(gameObject, _ttl);
        }

        public void Initialize(float damage)
        {
            this.damage = damage;
        }

        private void Update()
        {
            _rigidbody.velocity = transform.right * _speed;
        }


        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.transform.parent.TryGetComponent(out HealthComponent health))
                health.ModifyHealth(-damage);
        }
    }
}
