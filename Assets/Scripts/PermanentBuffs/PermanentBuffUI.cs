using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PermanentBuffs

{
    public class PermanentBuffUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private Image _icon;
        [SerializeField] private Slider _price;
        [SerializeField] private TextMeshProUGUI _essenceLeft;

        private PermanentBuff _gain;

        private void Awake()
        {
            _gain = GetComponent<PermanentBuff>();
            SetData();
        }

        private void Start()
        {
            _gain.OnChangeSpentEssence += Gain_OnChangeSpentEssence;
            GetComponent<Button>().onClick.AddListener(() => PermanentBuffWindow.Instance.SetGain(_gain));
        }

        private void Gain_OnChangeSpentEssence(object sender, EventArgs e)
        {
            SetEssence();
        }

        public void SetData()
        {
            _name.text = _gain.Data.Name;
            _icon.sprite = _gain.Data.Icon;
            SetEssence();
        }

        public void SetEssence()
        {
            _price.maxValue = _gain.Data.Price;
            _price.value = _gain.SpentEssence;
            int essenceLeft = _gain.Data.Price - _gain.SpentEssence;
            if (essenceLeft > 0)
                _essenceLeft.text = essenceLeft.ToString();
            else
                _essenceLeft.text = string.Empty;
        }
    }
}
