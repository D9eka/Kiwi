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

        private PermanentBuff _buff;

        private void Awake()
        {
            _buff = GetComponent<PermanentBuff>();
            SetData();
        }

        private void Start()
        {
            _buff.OnChangeSpentEssence += Gain_OnChangeSpentEssence;
            GetComponent<Button>().onClick.AddListener(() => PermanentBuffWindow.Instance.SetBuff(_buff));
        }

        private void Gain_OnChangeSpentEssence(object sender, EventArgs e)
        {
            SetEssence();
        }

        public void SetData()
        {
            _name.text = _buff.Data.Name;
            _icon.sprite = _buff.Data.Icon;
            SetEssence();
        }

        public void SetEssence()
        {
            _price.maxValue = _buff.Data.Price;
            _price.value = _buff.SpentEssence;
            int essenceLeft = _buff.Data.Price - _buff.SpentEssence;
            if (essenceLeft > 0)
                _essenceLeft.text = essenceLeft.ToString();
            else
                _essenceLeft.text = string.Empty;
        }
    }
}
