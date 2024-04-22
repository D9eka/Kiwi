using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace PermanentBuffs
{
    public class PermanentBuffWindow : MonoBehaviour
    {
        [SerializeField] PermanentBuff _activeBuff;
        [Space]
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private TextMeshProUGUI _description;
        [SerializeField] private Button _addEssenceButton;
        [SerializeField] private TextMeshProUGUI _essenceLeft;

        public static PermanentBuffWindow Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
            SetBuff(_activeBuff);
        }

        public void SetBuff(PermanentBuff buff)
        {
            if (_activeBuff != null)
                _activeBuff.OnChangeSpentEssence -= Buff_OnChangeSpentEssence;

            _activeBuff = buff;
            _activeBuff.OnChangeSpentEssence += Buff_OnChangeSpentEssence;
            Fill();
        }

        private void Buff_OnChangeSpentEssence(object sender, EventArgs e)
        {
            Fill();
        }

        private void Fill()
        {
            _name.text = _activeBuff.Data.Name;
            _description.text = _activeBuff.Data.Description;

            int essenceLeft = _activeBuff.Data.Price - _activeBuff.SpentEssence;
            if (essenceLeft <= 0)
            {
                _addEssenceButton.gameObject.SetActive(false);
            }
            else
            {
                _addEssenceButton.gameObject.SetActive(true);
                _essenceLeft.text = essenceLeft.ToString();
                if (GameManager.Instance.TrySpendEssence(_activeBuff.Data.Price - _activeBuff.SpentEssence))
                {
                    _addEssenceButton.interactable = true;
                    EventSystem.current.SetSelectedGameObject(_addEssenceButton.gameObject);
                }
                else
                    _addEssenceButton.interactable = false;
            }
            LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
        }

        public void TryBuy()
        {
            _activeBuff.SpendEssence();
        }
    }
}
