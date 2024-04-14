using System;
using System.Collections;
using System.Collections.Generic;
using Components.UI.Store;
using Creatures.Player;
using UnityEngine;
using UnityEngine.Serialization;
using Weapons;

public class WeaponChangeConfirmUI : MonoBehaviour
{
    [SerializeField] private WeaponBlockUI _weaponBlockFirst;

    [SerializeField] private WeaponBlockUI _weaponBlockSecond;

    // [SerializeField] private GameObject _content;
    private Weapon _weapon;
    public static WeaponChangeConfirmUI Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void Open(WeaponSO weaponSOOld, WeaponSO weaponSONew, Weapon weapon)
    {
        _weapon = weapon;
        _weaponBlockFirst.SetWeapon(weaponSOOld);
        _weaponBlockSecond.SetWeapon(weaponSONew);
        // _content.SetActive(true);
        UIManager.Instance.OpenNewWindow(GetComponent<WindowUI>());
    }

    public void Confirm()
    {
        WeaponController.Instance.EquipWeapon(_weaponBlockSecond.WeaponSO, _weapon);
        UIManager.Instance.TryCloseUntilCertainWindow(StoreUI.Instance.gameObject);
        StoreUI.Instance.Buy();
    }
}