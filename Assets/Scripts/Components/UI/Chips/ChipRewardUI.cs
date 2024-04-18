using Components.UI.Cards;
using System.Collections.Generic;
using UnityEngine;

namespace Components.UI.Screens
{
    public class ChipRewardUI : Screen
    {
        [SerializeField] private List<CardUI> _cards;

        private List<ChipSO> _chips = new();

        public static ChipRewardUI Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        private void GenerateChips()
        {
            _chips = Randomiser.GetRandomElements(ChipManager.Instance.PossibleChips, _cards.Count);
            for (int i = 0; i < _cards.Count; i++) 
            {
                _cards[i].Fill(_chips[i]);
            }
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