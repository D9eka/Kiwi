using System;
using UnityEngine;
using UnityEngine.UI;

public class WeaponInfoDisplayerUI : MonoBehaviour
{
    private Button _button;
    private WeaponIconUI _weaponIconUI;
    private WeaponBlockUI _weaponBlockUI;

    private void Start()
    {
        _weaponBlockUI = InventoryUI.Instance.WeaponBlockUI;
        _weaponIconUI = GetComponent<WeaponIconUI>();
        _button = GetComponent<Button>();
        _button.onClick.AddListener(DisplayInfo);
    }

    private void DisplayInfo()
    {
        _weaponBlockUI.SetWeapon(_weaponIconUI.WeaponSO);
        if (_weaponBlockUI.WeaponSO is null) return;
        _weaponBlockUI.gameObject.SetActive(true);
    }
}