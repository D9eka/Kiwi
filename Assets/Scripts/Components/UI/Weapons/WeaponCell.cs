using Components.UI;
using Components.UI.Screens;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Weapons;

public class WeaponCell : Cell
{
    [SerializeField] WeaponController.WeaponPosition _weaponPosition;
    [SerializeField] private WeaponCellPlace _place = WeaponCellPlace.HUD;

    private Weapon _currentWeapon;

    public enum WeaponCellPlace
    {
        HUD,
        Inventory
    }

    private void Start()
    {
        WeaponController.Instance.OnStateChange += WeaponController_OnStateChange;
        StartCoroutine(Initialize());
    }

    private void OnEnable()
    {
        StartCoroutine(Initialize());
    }

    private IEnumerator Initialize()
    {
        yield return new WaitForFixedUpdate();

        SetCurrentWeapon();
        if (_currentWeapon == null)
        {
            Fill(null);
            yield break;
        }

        Fill(_currentWeapon.Data.Icon, _place == WeaponCellPlace.HUD && _currentWeapon.Data.Type != WeaponSO.WeaponType.Melee, _currentWeapon.Label);
        Button button = GetComponent<Button>();
        button.onClick.RemoveAllListeners();
        switch (_place)
        {
            case WeaponCellPlace.HUD:
                button.onClick.AddListener(() => WeaponController.Instance.SwitchWeapon(_weaponPosition));
                break;
            case WeaponCellPlace.Inventory:
                button.onClick.AddListener(() => InventoryUI.Instance.Card.Fill(WeaponController.Instance.GetWeapon(_weaponPosition)));
                break;
            default:
                throw new NotImplementedException();
        }
    }

    private void SetCurrentWeapon()
    {
        _currentWeapon = WeaponController.Instance.GetWeapon(_weaponPosition);
    }

    private void WeaponController_OnStateChange(object sender, WeaponController.WeaponPosition e)
    {
        if (e == _weaponPosition && gameObject.activeInHierarchy)
            StartCoroutine(Initialize());
    }
}