using Components.UI.Cards;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Components.UI.Screens
{
    public class ChipRewardUI : ScreenComponent
    {
        [SerializeField] private List<CardUI> _cards;

        private List<ChipSO> _chips = new();
        private bool _canChangeProduct = StatsModifier.CanChangeReward;
        private const int CHANGE_PRODUCT_COST = 5;


        public EventHandler OnPlayerChooseChip;
        public static ChipRewardUI Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        protected override void Start()
        {
            base.Start();
            GenerateChips();
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
            if (!_canChangeProduct || !MyGameManager.TrySpendEssence(CHANGE_PRODUCT_COST))
                return;
            GenerateChips();
        }

        public void Obtain()
        {
            if (EventSystem.current.currentSelectedGameObject.TryGetComponent(out CardUI card))
                Obtain(_cards.IndexOf(card));
        }

        public void Obtain(int chipIndex)
        {
            ChipManager.Instance.ObtainChip(_chips[chipIndex]);
            OnPlayerChooseChip?.Invoke(this, EventArgs.Empty);
        }
    }
}