using Components.UI.Screens;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Components.UI.Chips
{
    public class ChipCell : Cell
    {
        [SerializeField] public ChipCellPosition _position;
        [SerializeField] private int _chipIndex = 0;

        private Chip _chip;

        public enum ChipCellPosition
        {
            Inventory,
            UpdateUI
        }

        private void Start()
        {
            ChipManager.Instance.OnStateChange += ChipManager_OnStateChange;
            StartCoroutine(Initialize());
        }

        private void ChipManager_OnStateChange(object sender, EventArgs e)
        {
            StartCoroutine(Initialize());
        }

        private IEnumerator Initialize()
        {
            yield return new WaitForFixedUpdate();
            Fill(null);
            if (_chipIndex < ChipManager.Instance.ObtainedChips.Count)
            {
                _chip = ChipManager.Instance.ObtainedChips[_chipIndex];
                Fill(_chip.ChipSO.Sprite);
                Button button = GetComponent<Button>();
                button.onClick.RemoveAllListeners();
                switch (_position)
                {
                    case ChipCellPosition.Inventory:
                        button.onClick.AddListener(() => InventoryUI.Instance.Card.Fill(_chip));
                        break;
                    case ChipCellPosition.UpdateUI:
                        button.onClick.AddListener(() => UpdateChipUI.Instance.Fill(_chip));
                        break;
                    default:
                        throw new NotImplementedException();
                }
            }
        }
    }
}
