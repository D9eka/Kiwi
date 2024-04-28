using Components.UI.Cards;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Weapons;

namespace Components.UI.Screens.Store
{
    public class StoreUI : ScreenComponent
    {
        [SerializeField] private List<CardUI> _cardsList;
        [SerializeField, Range(0, 3)] private int _consumableWeaponsCount = 1;
        [SerializeField] private List<Weapon> _possibleWeaponList;
        [SerializeField] private GameObject _soldPhrase;

        private List<Weapon> _weaponsList = new();
        private int _currentIndex;
        private bool _canChangeProduct = StatsModifier.CanChangeReward;
        private const int CHANGE_PRODUCT_COST = 5;

        public static StoreUI Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        protected override void Start()
        {
            base.Start();
            StartCoroutine(GenerateWeapons());
        }

        private IEnumerator GenerateWeapons()
        {
            yield return new WaitForFixedUpdate();
            Weapon[] equippedWeapons = WeaponController.Instance.EquippedWeapons;
            List<Weapon> allowedWeapons = _possibleWeaponList.Where(weapon => !equippedWeapons.Contains(weapon)).ToList();

            _weaponsList = new List<Weapon>();
            for (int i = 0; i < _cardsList.Count; i++)
            {
                Weapon weapon;
                List<Weapon> traps = allowedWeapons.Where(weapon => weapon.Data.Type == WeaponSO.WeaponType.Trap).ToList();
                List<Weapon> otherWeapon = allowedWeapons.Where(weapon => weapon.Data.Type != WeaponSO.WeaponType.Trap).ToList();
                if (_cardsList.Count - i - 1 < _consumableWeaponsCount && traps.Count > 0)
                {
                    weapon = Randomiser.GetRandomElement(traps);
                }
                else
                {
                    weapon = Randomiser.GetRandomElement(otherWeapon);
                }
                _weaponsList.Add(weapon);
                allowedWeapons.Remove(weapon);

                _cardsList[i].Fill(weapon);
                _cardsList[i].GetComponent<Button>().onClick.RemoveAllListeners();
                _cardsList[i].GetComponent<Button>().onClick.AddListener(() => TryBuy(i));
            }
        }

        public void TryChangeProduct()
        {
            if (!_canChangeProduct || !MyGameManager.TrySpendEssence(CHANGE_PRODUCT_COST))
                return;
            GenerateWeapons();
        }

        public void TryBuy()
        {
            if (EventSystem.current.currentSelectedGameObject.TryGetComponent(out CardUI card))
                TryBuy(_cardsList.IndexOf(card));
        }

        public void TryBuy(int weaponIndex)
        {
            if (!MyGameManager.CanSpendEssence(_weaponsList[weaponIndex].Data.Price))
                return;
            WeaponController.Instance.TryEquipWeapon(_weaponsList[weaponIndex]);
        }

        private void ShowSoldOut()
        {
            _soldPhrase.SetActive(true);
        }

        public void Buy()
        {
            MyGameManager.TrySpendEssence(_weaponsList[_currentIndex].Data.Price);
            _cardsList[_currentIndex].gameObject.SetActive(false);
            _canChangeProduct = false;
            var isAnyNotSold = _cardsList.Any(cardUI => cardUI.isActiveAndEnabled);
            if (!isAnyNotSold)
                ShowSoldOut();
        }
    }
}