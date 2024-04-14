using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Weapons;

public class WeaponControllerUI : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Image _firstWeaponImage;
    [SerializeField] private TextMeshProUGUI _firstWeaponText;
    [SerializeField] private Image _secondWeaponImage;
    [SerializeField] private TextMeshProUGUI _secondWeaponText;
    [SerializeField] private Image _trapImage;
    [SerializeField] private TextMeshProUGUI _trapText;
    private WeaponController _weaponController;

    private void Start()
    {
        _weaponController = WeaponController.Instance;
        _weaponController.OnStateChange += OnWeaponControllerStateChange;
    }

    private void OnWeaponControllerStateChange(object sender, EventArgs e)
    {
        SetAllWeaponsInfo();
    }

    private void SetAllWeaponsInfo()
    {
        SetSingleWeaponInfo(_weaponController.FirstWeapon, _firstWeaponImage, _firstWeaponText);
        SetSingleWeaponInfo(_weaponController.SecondWeapon, _secondWeaponImage, _secondWeaponText);
        SetSingleWeaponInfo(_weaponController.Trap, _trapImage, _trapText);
    }

    private void SetSingleWeaponInfo(Weapon weapon, Image image, TMP_Text textMeshProUGUI)
    {
        if (weapon is null)
        {
            image.sprite = null;
            textMeshProUGUI.text = null;
            return;
        }

        if (weapon.WeaponSO is not null)
        {
            var weaponSO = weapon.WeaponSO;
            image.sprite = weaponSO.Sprite;
        }

        textMeshProUGUI.text = weapon switch
        {
            Gun gun => gun.AmmoCount + "/" + gun.AmmoCapacity,
            Trap trap => trap.Amount.ToString(),
            _ => ""
        };
    }
}