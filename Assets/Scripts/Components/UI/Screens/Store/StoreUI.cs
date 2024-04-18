using Components.UI.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using Weapons;

namespace Components.UI.Screens.Store
{
    public class StoreUI : Screen
    {
        [SerializeField] private List<CardUI> _cardsList;
        [SerializeField, Range(0, 3)] private int _consumableWeaponsCount = 1;
        [SerializeField] private List<Weapon> _possibleWeaponList;
        [SerializeField] private GameObject _soldPhrase;

        private List<Weapon> weaponsList = new();
        private int _currentIndex;
        private bool _canChangeProduct = StatsModifier.CanChangeReward;
        private int _changeProductCost = 5;

        public static StoreUI Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            GenerateWeapons();
        }

        private void GenerateWeapons()
        {
            var consumableWeapons =
                Randomiser.GetRandomElements(_possibleWeaponList.FindAll(
                    weapon => weapon.Data.Type == WeaponSO.WeaponType.Trap), _consumableWeaponsCount);
            var notConsumableWeapons = Randomiser.GetRandomElements(_possibleWeaponList.FindAll(
                weapon => weapon.Data.Type != WeaponSO.WeaponType.Trap), _cardsList.Count - _consumableWeaponsCount);
            
            var i = 0;
            foreach (var card in _cardsList)
            {
                if (i < notConsumableWeapons.Count)
                {
                    weaponsList.Add(notConsumableWeapons[i]);
                    card.Fill(notConsumableWeapons[i], true);
                    i++;
                    continue;
                }

                weaponsList.Add(consumableWeapons[i - notConsumableWeapons.Count]);
                card.Fill(consumableWeapons[i - notConsumableWeapons.Count], true);
            }
        }

        public void TryChangeProduct()
        {
            if (!_canChangeProduct) 
                return;
            if (!GameManager.Instance.TrySpendEssence(_changeProductCost)) 
                return;
            GenerateWeapons();
        }

        public void TryBuy(CardUI card)
        {
            _currentIndex = _cardsList.IndexOf(card);
            if (!GameManager.Instance.CanSpendEssence(weaponsList[_currentIndex].Data.Price))
                return;
            WeaponController.Instance.TryEquipWeapon(weaponsList[_currentIndex]);
        }

        public override void Exit()
        {
            Trader.Instance.Disable();
            base.Exit();
        }

        private void ShowSoldOut()
        {
            _soldPhrase.SetActive(true);
        }

        public void Buy()
        {
            GameManager.Instance.TrySpendEssence(weaponsList[_currentIndex].Data.Price);
            _cardsList[_currentIndex].gameObject.SetActive(false);
            _canChangeProduct = false;
            var isAnyNotSold = _cardsList.Any(cardUI => cardUI.isActiveAndEnabled);
            if (!isAnyNotSold) 
                ShowSoldOut();
        }
    }
}