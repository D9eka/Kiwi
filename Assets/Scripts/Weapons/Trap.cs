using Creatures.Player;
using UnityEngine;

namespace Weapons
{
    public class Trap : Weapon
    {
        private int _amount;
        public int Amount => _amount;

        private void Awake()
        {
            _amount = _data.MaxAmount;
            Label = _amount.ToString();
        }

        public override void Attack()
        {
            base.Attack();

            if (_timeBetweenAttacks < _data.ThrownDelay || _amount <= 0)
                return;
        }

        public void OnAttack()
        {
            float playerLocalScaleX = PlayerController.Instance.transform.localScale.x;
            Instantiate(_data.ThrownTrapPrefab, PlayerController.Instance.ThrownTrapHandler.position, Quaternion.identity);
            _timeBetweenAttacks = 0;

            _amount--;
            Label = _amount.ToString();
            if (_amount <= 0)
            {
                WeaponController.Instance.DropTrap();
            }
        }
    }
}