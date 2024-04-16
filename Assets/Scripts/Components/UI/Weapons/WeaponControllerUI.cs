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
    [SerializeField] private WeaponIconUI _weaponIconFirst;
    [SerializeField] private WeaponIconUI _weaponIconSecond;
    [SerializeField] private WeaponIconUI _trapIcon;
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
        _weaponIconFirst.SetInfo(_weaponController.FirstWeapon);
        _weaponIconSecond.SetInfo(_weaponController.SecondWeapon);
        _trapIcon.SetInfo(_weaponController.Trap);
    }
}