using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Weapons;

namespace Components.UI.Cards
{
    public class CardUI : MonoBehaviour
    {
        [SerializeField] private RectTransform _content;
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private Image _icon;
        [Space]
        [SerializeField] private GameObject _descrHandler;
        [SerializeField] private TextMeshProUGUI _descr;
        [Space]
        [SerializeField] private GameObject _paramsHandler;
        [SerializeField] private GameObject _labelsHandler;
        [SerializeField] private GameObject _valuesHandler;
        [SerializeField] private GameObject _paramsTextPrefab;

        public void Fill(Weapon weapon, bool havePrice = false)
        {
            WeaponSO data = weapon.Data;
            List<string> labels = new() { "Урон", "Скорость", "Дальность", "Боезапас", "Тип урона" };
            List<string> values = new() { data.Damage.ToString(), System.Math.Round(data.AttackSpeed, 1).ToString(), data.Range, data.Ammo, data.DamageTypeUI };
            if (havePrice)
            {
                labels.Add("Цена");
                values.Add(data.Price.ToString());
            }
            Fill(data.Name, data.Icon, data.Description != "", data.Description, true, labels, values);
        }

        public void Fill(ChipSO chipSO)
        {
            Fill(chipSO.Name, chipSO.Sprite, true, chipSO.Description);
        }

        public void Fill(Chip chip, bool nextLevel = false)
        {
            List<string> labels = new() { "Уровень" };
            int level = chip.CurrentLevel;
            if (nextLevel && level < chip.ChipSO.Descriptions.Count)
                level++;
            string description = chip.ChipSO.Descriptions[level - 1];
            List<string> values = new() { level.ToString() };
            Fill(chip.ChipSO.Name, chip.ChipSO.Sprite,
                description != "", description,
                true, labels, values);
        }

        public void Fill(SectionTypeSO sectionTypeSO)
        {
            Fill(sectionTypeSO.Name, null);
        }

        public void Fill(string name, Sprite icon,
                         bool haveDescr = false, string descr = null,
                         bool haveParams = false, List<string> labels = null, List<string> values = null)
        {
            _name.text = name;
            _icon.transform.parent.gameObject.SetActive(icon != null);
            if (icon != null)
                _icon.sprite = icon;

            _descrHandler.SetActive(haveDescr);
            if (haveDescr)
                _descr.text = descr;
            LayoutRebuilder.ForceRebuildLayoutImmediate(_descrHandler.GetComponent<RectTransform>());

            _paramsHandler.SetActive(haveParams);
            if (haveParams)
            {
                FillParent(_labelsHandler.transform, labels);
                FillParent(_valuesHandler.transform, values);
            }
            _content.gameObject.SetActive(false);
            _content.gameObject.SetActive(true);
        }

        private void FillParent(Transform parent, List<string> values)
        {
            foreach (Transform go in parent.GetComponentsInChildren<Transform>())
                if (go != parent)
                    Destroy(go.gameObject);

            foreach (string value in values)
            {
                GameObject go = Instantiate(_paramsTextPrefab, parent);
                go.GetComponent<TextMeshProUGUI>().text = value;
            }
        }
    }
}
