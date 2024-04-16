using System;
using System.Collections.Generic;
using System.Linq;
using Creatures.Player;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Components.UI.Store
{
    public class StoreUI : MonoBehaviour
    {
        [SerializeField] private List<WeaponBlockUI> _weaponBlockList;
        [SerializeField, Range(0, 3)] private int _consumableWeaponsCount = 1;
        [SerializeField] private List<WeaponSO> _possibleWeaponList;
        [SerializeField] private GameObject _soldPhrase;
        private bool _canChangeProduct = true;
        private int _changeProductCost = 1;
        private WeaponBlockUI _currentWeaponBlockUI;

        // private int _cost = 1;

        // [SerializeField] private GameObject _content;
        public static StoreUI Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            GenerateWeapons();
        }

        public void Open()
        {
            UIManager.Instance.OpenNewWindow(GetComponent<WindowUI>());
        }

        private void GenerateWeapons()
        {
            var consumableWeapons =
                Randomiser.GetRandomElements(_possibleWeaponList.FindAll(x => x.IsConsumable), _consumableWeaponsCount);
            var notConsumableWeapons = Randomiser.GetRandomElements(_possibleWeaponList.FindAll(x => !x.IsConsumable),
                _weaponBlockList.Count - _consumableWeaponsCount);
            var i = 0;
            foreach (var weaponBlock in _weaponBlockList)
            {
                if (i < notConsumableWeapons.Count)
                {
                    weaponBlock.SetWeapon(notConsumableWeapons[i]);
                    i++;
                    continue;
                }

                weaponBlock.SetWeapon(consumableWeapons[i - notConsumableWeapons.Count]);
            }
        }

        public void TryChangeProduct()
        {
            if (!_canChangeProduct) return;
            if (!GameManager.Instance.TrySpendEssence(_changeProductCost)) return;
            GenerateWeapons();
        }

        public void TryBuy(WeaponBlockUI weaponBlock)
        {
            _currentWeaponBlockUI = weaponBlock;
            if (!GameManager.Instance.CanSpendEssence(weaponBlock.Cost)) return;
            WeaponController.Instance.TryEquipWeapon(weaponBlock.WeaponSO);
            // WeaponController.Instance.TryEquipWeapon(weaponBlock.WeaponSO);
            // Buy();
        }

        private void CloseStore()
        {
            UIManager.Instance.CloseAllWindows();
            Trader.Instance.Disable();
        }

        private void ShowSoldOut()
        {
            _soldPhrase.SetActive(true);
        }

        public void Buy()
        {
            GameManager.Instance.TrySpendEssence(_currentWeaponBlockUI.Cost);
            _currentWeaponBlockUI.gameObject.SetActive(false);
            _canChangeProduct = false;
            var isAnyNotSold = _weaponBlockList.Any(blockUI => blockUI.isActiveAndEnabled);
            if (!isAnyNotSold) ShowSoldOut();
        }
    }
}