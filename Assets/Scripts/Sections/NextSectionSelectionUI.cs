using Components.UI.Cards;
using Sections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Components.UI.Screens
{
    public class NextSectionSelectionUI : ScreenComponent
    {
        [SerializeField] private List<CardUI> _cardsList;

        private SectionTypeSO[] _sectionTypes;
        public static NextSectionSelectionUI Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
            _sectionTypes = new SectionTypeSO[_cardsList.Count];
        }

        public void SetTypes(List<SectionTypeSO> sectionTypes)
        {
            _sectionTypes = sectionTypes.ToArray();
            for (int i = 0; i < _cardsList.Count; i++)
            {
                if (i < sectionTypes.Count)
                {
                    _cardsList[i].gameObject.SetActive(true);
                    _cardsList[i].Fill(sectionTypes[i]);
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
            SetTypes(SectionManager.Instance.GetRandomSectionTypes());
        }

        public void Choose()
        {
            if (EventSystem.current.currentSelectedGameObject != null &&
                EventSystem.current.currentSelectedGameObject.TryGetComponent(out CardUI card))
                Choose(_cardsList.IndexOf(card));
        }

        public void Choose(int sectionIndex)
        {
            if (sectionIndex < _cardsList.Count)
            {
                SectionManager.Instance.EnterNextSection(_sectionTypes[sectionIndex]);
                UIController.Instance.PopScreen();
            }
        }
    }
}