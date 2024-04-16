using System;
using System.Collections;
using System.Collections.Generic;
using Components.UI.Store;
using Creatures.Player;
using UnityEngine;
using UnityEngine.Serialization;
using Weapons;

public class WeaponGetConfirmUI : MonoBehaviour
{
    [SerializeField] private WeaponBlockUI _weaponBlock;

    // [SerializeField] private GameObject _content;
    private Weapon _weapon;
    public static WeaponGetConfirmUI Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void Open(WeaponSO weaponSO, Weapon weapon)
    {
        _weapon = weapon;
        _weaponBlock.SetWeapon(weaponSO);
        UIManager.Instance.OpenNewWindow(GetComponent<WindowUI>());
    }

    public void Confirm()
    {
        WeaponController.Instance.EquipWeapon(_weaponBlock.WeaponSO, _weapon);
        UIManager.Instance.TryCloseUntilCertainWindow(StoreUI.Instance.gameObject);
        StoreUI.Instance.Buy();
    }
}