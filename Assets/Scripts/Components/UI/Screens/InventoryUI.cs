using Components.UI.Cards;
using UnityEngine;

namespace Components.UI.Screens
{
    public class InventoryUI : ScreenComponent
    {
        public static InventoryUI Instance { get; private set; }
        [SerializeField] private CardUI _card;
        public CardUI Card
        {
            get
            {
                _card.gameObject.SetActive(true);
                return _card;
            }
        }

        private void Awake()
        {
            Instance = this;
        }

        public void HideInfoBlocks()
        {
            _card.gameObject.SetActive(false);
        }
    }
}