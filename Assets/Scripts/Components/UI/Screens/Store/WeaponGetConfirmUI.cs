using Components.UI.Cards;
using UnityEngine;
using Weapons;
using static WeaponController;

namespace Components.UI.Screens.Store
{
    public class WeaponGetConfirmUI : Screen
    {
        [SerializeField] private CardUI _card;

        private Weapon _weapon;
        private WeaponPosition _weaponPos;

        public static WeaponGetConfirmUI Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        public void Open(Weapon weapon, WeaponPosition weaponPos)
        {
            _weapon = weapon;
            _weaponPos = weaponPos;

            _card.Fill(weapon);
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