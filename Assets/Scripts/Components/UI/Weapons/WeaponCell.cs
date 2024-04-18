using Components.UI;
using Components.UI.Screens;
using System;
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
        SetCurrentWeapon();
        Fill(_currentWeapon.Data.Icon);

        WeaponController.Instance.OnStateChange += WeaponController_OnStateChange;

        Button button = GetComponent<Button>();
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

    protected override void Fill(Sprite icon, bool needLabel = false, string label = null)
    {
        _icon.color = _currentWeapon == null ? new Color(0, 0, 0, 0) : Color.white;
        base.Fill(icon, needLabel, label);
    }

    private void WeaponController_OnStateChange(object sender, WeaponController.WeaponPosition e)
    {
        if(e == _weaponPosition)
        {
            SetCurrentWeapon();
            Fill(_currentWeapon.Data.Icon);
        }
    }
}