using Components.Health;
using Creatures.Player;
using UnityEngine;

namespace Weapons
{
    public class Trap : Weapon
    {
        [SerializeField] private ThrownTrap _thrownTrap;
        [SerializeField] private Vector2 _throwForce;

        private int _amount;

        private void Start()
        {
            _amount = _data.MaxAmount;
        }

        public override void Attack()
        {
            base.Attack();

            if (_timeBetweenAttacks < _data.ThrownDelay || _amount <= 0)
                return;

            float playerLocalScaleX = PlayerController.Instance.transform.localScale.x;
            Vector2 throwForce = _throwForce * playerLocalScaleX;
            ThrownTrap thrownTrap = Instantiate(_thrownTrap, transform.position, Quaternion.identity)
                .GetComponent<ThrownTrap>();
            thrownTrap.Initialize(_currentDamage, _data.AttackDelay, _data.DestroyType, _data.TTLSeconds, throwForce);
            _timeBetweenAttacks = 0;

            _amount--;
            if (_amount <= 0)
            {
                WeaponController.Instance.DropTrap();
            }
        }
    }
}