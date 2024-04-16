using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI Instance { get; private set; }
    [SerializeField] private WeaponBlockUI _weaponBlockUI;
    public WeaponBlockUI WeaponBlockUI => _weaponBlockUI;

    private void Awake()
    {
        Instance = this;
    }


    public void HideInfoBlocks()
    {
        _weaponBlockUI.gameObject.SetActive(false);
    }
}