using Components.UI.Cards;
using UnityEngine;
using Weapons;
using static WeaponController;

namespace Components.UI.Screens.Store
{
    public class WeaponChooseUI : Screen
    {
        [SerializeField] private CardUI _weaponFirst;
        [SerializeField] private CardUI _weaponSecond;

        private Weapon _weapon;

        public static WeaponChooseUI Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }
        public void Open(Weapon weapon)
        {
            _weapon = weapon;
            _weaponFirst.Fill(WeaponController.Instance.GetWeapon(WeaponPosition.Weapon1));
            _weaponSecond.Fill(WeaponController.Instance.GetWeapon(WeaponPosition.Weapon2));
            UIController.Instance.PushScreen(this);
        }

        public void Choose(CardUI weaponCard)
        {
            WeaponPosition weaponPos = weaponCard == _weaponFirst ? WeaponPosition.Weapon1 : WeaponPosition.Weapon2;
            WeaponChangeConfirmUI.Instance.Open(_weapon, weaponPos);
        }
    }
}