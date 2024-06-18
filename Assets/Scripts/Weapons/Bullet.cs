using Components.Health;
using Creatures.Player;
using UnityEngine;

namespace Weapons
{
    public class Bullet : MonoBehaviour
    {
        private Rigidbody2D _rigidbody;

        private float _damage;
        private float _speed;

        private bool _isEnergyDamage;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        public void Initialize(float damage, float speed, float ttl, bool isEnergyDamage)
        {
            this._damage = damage;
            this._speed = speed;
            Destroy(gameObject, ttl);
            this._isEnergyDamage = isEnergyDamage;
        }

        private void Update()
        {
            _rigidbody.velocity = transform.right * _speed;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.isTrigger)
            {
                if (_isEnergyDamage && collision.TryGetComponent(out ControlPanel controlPanel))
                    controlPanel.TryOpenSecretDoor(false);
                else if (_isEnergyDamage && collision.TryGetComponent(out GravityPanel gravityPanel))
                    gravityPanel.InvertGravity();
                else
                    return;
            }

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
