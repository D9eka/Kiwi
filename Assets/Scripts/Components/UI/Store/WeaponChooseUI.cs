using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Weapons;

public class WeaponChooseUI : MonoBehaviour
{
    [SerializeField] private WeaponBlockUI _weaponBlockFirst;

    [SerializeField] private WeaponBlockUI _weaponBlockSecond;

    // [SerializeField] private GameObject _content;
    private WeaponSO _newWeapon;

    public static WeaponChooseUI Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
    public void Open(WeaponSO newWeapon)
    {
        _weaponBlockFirst.SetWeapon(WeaponController.Instance.FirstWeapon.WeaponSO);
        _weaponBlockSecond.SetWeapon(WeaponController.Instance.SecondWeapon.WeaponSO);
        _newWeapon = newWeapon;
        UIManager.Instance.OpenNewWindow(GetComponent<WindowUI>());
    }

    public void Choose(WeaponBlockUI weaponBlock)
    {
        var weapon = weaponBlock == _weaponBlockFirst
            ? WeaponController.Instance.FirstWeapon
            : WeaponController.Instance.SecondWeapon;
        WeaponChangeConfirmUI.Instance.Open(weaponBlock.WeaponSO, _newWeapon, weapon);
    }
}