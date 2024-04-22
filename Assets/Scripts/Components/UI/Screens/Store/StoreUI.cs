using Components.UI.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using Weapons;

namespace Components.UI.Screens.Store
{
    public class StoreUI : ScreenComponent
    {
        [SerializeField] private List<CardUI> _cardsList;
        [SerializeField, Range(0, 3)] private int _consumableWeaponsCount = 1;
        [SerializeField] private List<Weapon> _possibleWeaponList;
        [SerializeField] private GameObject _soldPhrase;

        private List<Weapon> weaponsList = new();
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
            GenerateWeapons();
        }

        protected override void FillEventButtons()
        {
            if (_haveEventButtons)
            {
                foreach (Transform go in _eventsHandler.GetComponentsInChildren<Transform>())
                    Destroy(go.gameObject);

                if (_cancelEvent != null)
                    CreateEventButton(CANCEL_EVENT_KEY, _cancelEvent);
                if (_additionlEvent != null && _canChangeProduct)
                    CreateEventButton(ADDITIONAL_EVENT_KEY, _additionlEvent);
                if (_confirmEvent != null)
                    CreateEventButton(CONFIRM_EVENT_KEY, _confirmEvent);
            }
        }

        private void GenerateWeapons()
        {
            var consumableWeapons =
                Randomiser.GetRandomElements(_possibleWeaponList.FindAll(
                    weapon => weapon.Data.Type == WeaponSO.WeaponType.Trap), _consumableWeaponsCount);
            var notConsumableWeapons = Randomiser.GetRandomElements(_possibleWeaponList.FindAll(
                weapon => weapon.Data.Type != WeaponSO.WeaponType.Trap), _cardsList.Count - _consumableWeaponsCount - 1);
            
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
            if (!GameManager.Instance.TrySpendEssence(CHANGE_PRODUCT_COST)) 
                return;
            GenerateWeapons();
        }

        public void TryBuy()
        {
            if (EventSystem.current.currentSelectedGameObject.TryGetComponent(out CardUI card))
                TryBuy(card);
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