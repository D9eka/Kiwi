﻿using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Components.UI
{
    public class Cell : MonoBehaviour
    {
        [SerializeField] protected Image _icon;
        [SerializeField] protected GameObject _labelHandler;
        [SerializeField] protected TextMeshProUGUI _label;

        protected virtual void Fill(Sprite icon, bool needLabel = false, string label = null)
        {
            _icon.color = icon == null ? new Color(0, 0, 0, 0) : Color.white;
            _icon.sprite = icon;
            _labelHandler.SetActive(needLabel);
            if (needLabel)
            {
                _label.text = label;
                _labelHandler.GetComponent<Image>().color = label[0] == '0' ? Color.red : Color.white;
            }
        }
    }
}
