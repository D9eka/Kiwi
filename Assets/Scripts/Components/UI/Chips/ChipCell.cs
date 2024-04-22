using System;
using UnityEngine;

namespace Components.UI.Chips
{
    public class ChipCell : Cell
    {
        [SerializeField] private int _chipIndex = 0;

        private Chip _chip;

        private void Start()
        {
            ChipManager.Instance.OnStateChange += ChipManager_OnStateChange;
        }

        private void ChipManager_OnStateChange(object sender, EventArgs e)
        {
            Initialize();
        }

        private void Initialize()
        {
            Fill(null);
            if (_chipIndex < ChipManager.Instance.ObtainedChips.Count)
            {
                _chip = ChipManager.Instance.ObtainedChips[_chipIndex];
                Fill(_chip.ChipSO.Sprite);
            }
        }
    }
}
