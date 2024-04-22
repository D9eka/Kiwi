using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Weapons;

namespace Components.UI.Cards
{
    public class CardUI : MonoBehaviour
    {
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

        public void Fill(Weapon weapon, bool havePrice=false)
        {
            WeaponSO data = weapon.Data;
            List<string> labels = new() { "Урон", "Скорость", "Дальность", "Боезапас", "Тип урона" };
            List<string> values = new() { data.Damage.ToString(), data.AttackSpeed.ToString(), data.Range, data.Ammo, data.DamageTypeUI };
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

        public void Fill(string name, Sprite icon, 
                         bool haveDescr=false, string descr=null,
                         bool haveParams=false, List<string> labels = null, List<string> values = null)
        {
            _name.text = name;
            _icon.sprite = icon;

            _descrHandler.SetActive(haveDescr);
            if (haveDescr)
                _descr.text = descr;

            _paramsHandler.SetActive(haveParams);
            if (haveParams)
            {
                FillParent(_labelsHandler.transform, labels);
                FillParent(_valuesHandler.transform, values);
            }
        }

        private void FillParent(Transform parent, List<string> values)
        {
            foreach (Transform go in parent.GetComponentsInChildren<Transform>())
                    if(go != parent)
                        Destroy(go.gameObject);

            foreach (string value in values)
            {
                GameObject go = Instantiate(_paramsTextPrefab, parent);
                go.GetComponent<TextMeshProUGUI>().text = value;
            }
        }
    }
}
