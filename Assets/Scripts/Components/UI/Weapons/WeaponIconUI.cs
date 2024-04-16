using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Weapons;

public class WeaponIconUI : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _text;
    public WeaponSO WeaponSO { get; private set; }

    public void SetInfo(Weapon weapon)
    {
        if (weapon is null)
        {
            _image.sprite = null;
            if (_text is null) return;
            _text.text = null;
            return;
        }

        if (weapon.WeaponSO is not null)
        {
            WeaponSO = weapon.WeaponSO;
            _image.sprite = WeaponSO.Sprite;
        }

        if (_text is null) return;
        _text.text = weapon switch
        {
            Gun gun => gun.AmmoCount + "/" + gun.AmmoCapacity,
            Trap trap => trap.Amount.ToString(),
            _ => ""
        };
    }
}