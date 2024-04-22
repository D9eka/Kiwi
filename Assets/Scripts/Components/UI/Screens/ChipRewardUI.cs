using Components.UI.Cards;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Components.UI.Screens
{
    public class ChipRewardUI : ScreenComponent
    {
        [SerializeField] private List<CardUI> _cards;

        private List<ChipSO> _chips = new();
        private bool _canChangeProduct = StatsModifier.CanChangeReward;
        private const int CHANGE_PRODUCT_COST = 5;

        public static ChipRewardUI Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
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

        private void GenerateChips()
        {
            _chips = Randomiser.GetRandomElements(ChipManager.Instance.PossibleChips, _cards.Count);
            for (int i = 0; i < _cards.Count; i++) 
            {
                _cards[i].Fill(_chips[i]);
            }
        }

        public void TryChangeProduct()
        {
            if (!_canChangeProduct)
                return;
            if (!GameManager.Instance.TrySpendEssence(CHANGE_PRODUCT_COST))
                return;
            GenerateChips();
        }

        public void Obtain()
        {
            if (EventSystem.current.currentSelectedGameObject.TryGetComponent(out CardUI card))
                Obtain(card);
        }

        public void Obtain(CardUI card)
        {
            ChipManager.Instance.ObtainChip(_chips[_cards.IndexOf(card)]);
            UIController.Instance.PopScreen();
        }

        public void ShowReward()
        {
            GenerateChips();
            UIController.Instance.PushScreen(this);
        }
    }
}