using Components.UI.Cards;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Components.UI.Screens
{
    public class ChipRewardUI : ScreenComponent
    {
        [SerializeField] private List<CardUI> _cardsList;

        private List<ChipSO> _chips;

        public EventHandler OnPlayerChooseChip;
        public static ChipRewardUI Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
            _chips = new();
        }

        protected override void Start()
        {
            base.Start();
            GenerateChips();
        }

        private void GenerateChips()
        {
            _chips = Randomiser.GetRandomElements(ChipManager.Instance.PossibleChips, _cardsList.Count);
            for (int i = 0; i < _cardsList.Count; i++)
            {
                if (i < _chips.Count)
                {
                    _cardsList[i].gameObject.SetActive(true);
                    _cardsList[i].Fill(_chips[i]);
                }
                else
                {
                    _cardsList[i].gameObject.SetActive(false);
                }
            }
        }

        public void TryChangeProduct()
        {
            if (!StatsModifier.CanChangeReward || !MyGameManager.TrySpendEssence(MyGameManager.CHANGE_REWARD_COST))
                return;
            GenerateChips();
        }

        public void Obtain()
        {
            if (EventSystem.current.currentSelectedGameObject != null &&
                EventSystem.current.currentSelectedGameObject.TryGetComponent(out CardUI card))
                Obtain(_cardsList.IndexOf(card));
        }

        public void Obtain(int chipIndex)
        {
            if (chipIndex < _cardsList.Count)
            {
                ChipManager.Instance.ObtainChip(_chips[chipIndex]);
                OnPlayerChooseChip?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}