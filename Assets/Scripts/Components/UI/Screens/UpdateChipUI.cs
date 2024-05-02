using Components.UI.Cards;
using TMPro;
using UnityEngine;

namespace Components.UI.Screens
{
    public class UpdateChipUI : ScreenComponent
    {
        [SerializeField] private CardUI _soloCard;
        [SerializeField] private GameObject _updateBlock;
        [SerializeField] private CardUI _currentLevelCard;
        [SerializeField] private CardUI _nextLevelCard;
        [SerializeField] private TextMeshProUGUI _updatePriceText;

        private Chip _chip;
        private int _price;
        private bool _firstUse = true;

        public static UpdateChipUI Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        protected override void Start()
        {
            base.Start();
            SetPrice();
        }

        public void Fill(Chip chip)
        {
            _chip = chip;
            Fill();
        }

        private void Fill()
        {
            if (_chip == null)
                return;

            _soloCard.gameObject.SetActive(false);
            _updateBlock.gameObject.SetActive(false);
            if (!_chip.CanBeUpgrade)
            {
                _soloCard.gameObject.SetActive(true);
                _soloCard.Fill(_chip);
            }
            else
            {
                _updateBlock.SetActive(true);
                _currentLevelCard.Fill(_chip);
                _nextLevelCard.Fill(_chip, true);
            }
        }

        private void SetPrice()
        {
            _price = _firstUse ? 0 : MyGameManager.UPDATE_CHIP_COST;
            _updatePriceText.text = _price.ToString();
        }

        public void TryUpgrade()
        {
            if (_chip == null || !_chip.CanBeUpgrade || !MyGameManager.TrySpendEssence(_price))
                return;
            _chip.TryUpgrade();
            _firstUse = false;
            SetPrice();
            Fill();
        }
    }
}
