using Components.UI.Cards;
using UnityEngine;
using Weapons;
using static WeaponController;

namespace Components.UI.Screens.Store
{
    public class WeaponChangeConfirmUI : ScreenComponent
    {
        [SerializeField] private CardUI _weaponBlockFirst;
        [SerializeField] private CardUI _weaponBlockSecond;

        private Weapon _weapon;
        private WeaponPosition _weaponPos;
        public static WeaponChangeConfirmUI Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        public void Open(Weapon weapon, WeaponPosition position)
        {
            _weapon = weapon;
            _weaponPos = position;
            _weaponBlockFirst.Fill(WeaponController.Instance.GetWeapon(position));
            _weaponBlockSecond.Fill(weapon);
            UIController.Instance.PushScreen(this);
        }

        public void Confirm()
        {
            WeaponController.Instance.EquipWeapon(_weapon, _weaponPos);
            StoreUI.Instance.Buy();
            UIController.Instance.PopAllScreens();
        }
    }
}