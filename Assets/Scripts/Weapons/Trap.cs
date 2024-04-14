using Components.Health;
using Creatures.Player;
using UnityEngine;

namespace Weapons
{
    public class Trap : Weapon
    {
        [SerializeField] private ThrownTrap _thrownTrap;
        [SerializeField] private float _thrownTrapAttackDelay;
        [SerializeField] private Vector2 _throwForce;

        [SerializeField] private int _maxAmount;

        private int _amount;
        public int Amount => _amount;
        public int MaxAmount => _maxAmount;

        private void Start()
        {
            _amount = _maxAmount;
        }

        public override void Attack()
        {
            base.Attack();

            if (_timeBetweenAttacks < _attackDelay || _amount <= 0)
                return;

            float playerLocalScaleX = PlayerController.Instance.transform.localScale.x;
            Vector2 throwForce = _throwForce * playerLocalScaleX;
            ThrownTrap thrownTrap = Instantiate(_thrownTrap, transform.position, Quaternion.identity)
                .GetComponent<ThrownTrap>();
            thrownTrap.Initialize(_currentDamage, _thrownTrapAttackDelay, throwForce);
            _timeBetweenAttacks = 0;

            _amount--;
            if (_amount <= 0)
            {
                // gameObject.SetActive(false);
                if (WeaponController.Instance is not null) WeaponController.Instance.DropConsumableWeapon();
            }
        }
    }
}