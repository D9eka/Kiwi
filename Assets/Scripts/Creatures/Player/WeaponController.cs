using System;
using Components.UI.Screens.Store;
using Creatures.Player;
using UnityEngine;
using UnityEngine.UIElements;
using Weapons;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private Transform _weaponHandler;
    [SerializeField] private GameObject _firstWeapon;
    [SerializeField] private GameObject _secondWeapon;
    [SerializeField] private GameObject _trap;

    private GameObject[] _weaponArray = new GameObject[3];
    private int _currentWeapon;
    public Weapon CurrentWeapon => _weaponArray[_currentWeapon] ? _weaponArray[_currentWeapon].GetComponent<Weapon>() : null;
    public event EventHandler<WeaponPosition> OnStateChange;
    public static WeaponController Instance { get; private set; }

    public enum WeaponPosition
    {
        Weapon1 = 0,
        Weapon2 = 1,
        Trap = 2
    }

    private void Awake()
    {
        Instance = this;

        int i = 0;
        foreach (GameObject weapon in new GameObject[]{ _firstWeapon, _secondWeapon, _trap })
        {
            if (weapon != null)
            {
                GameObject weaponGO = Instantiate(weapon, _weaponHandler);
                _weaponArray[i] = weaponGO;
                weaponGO.SetActive(false);
            }
            i++;
        }
        SwitchWeapon();
    }

    public void SwitchWeapon(int weaponIndex)
    {
        _currentWeapon = weaponIndex;
        _weaponArray[_currentWeapon].SetActive(true);
    }
    public void SwitchWeapon()
    {
        do
            _currentWeapon = (int)Mathf.Repeat(_currentWeapon + 1f, _weaponArray.Length - 1);
        while (_weaponArray[_currentWeapon] == null);

        SwitchWeapon(_currentWeapon);
    }

    public void SwitchWeapon(WeaponPosition weaponPosition)
    {
        SwitchWeapon((int)weaponPosition);
    }

    private void Start()
    {
        var playerInputReader = PlayerInputReader.Instance;
        playerInputReader.OnSwitchWeapon += OnPlayerSwitchWeapon;
        playerInputReader.OnWeaponReload += OnPlayerWeaponReload;
        playerInputReader.OnAttack += OnPlayerAttack;
    }

    private void OnPlayerAttack(object sender, EventArgs e)
    {
        OnStateChange?.Invoke(this, (WeaponPosition)_currentWeapon);
    }

    private void OnPlayerWeaponReload(object sender, EventArgs e)
    {
        OnStateChange?.Invoke(this, (WeaponPosition)_currentWeapon);
    }

    private void OnPlayerSwitchWeapon(object sender, EventArgs e)
    {
        SwitchWeapon();
    }

    public Weapon GetWeapon(WeaponPosition position) 
        => _weaponArray[(int)position] ? _weaponArray[(int)position].GetComponent<Weapon>() : null; 

    public void TryEquipWeapon(Weapon weapon)
    {
        if(weapon.Data.Type == WeaponSO.WeaponType.Trap)
        {
            if (_weaponArray[(int)WeaponPosition.Trap])
            {
                WeaponChangeConfirmUI.Instance.Open(weapon, WeaponPosition.Trap);
            }
            else
            {
                WeaponGetConfirmUI.Instance.Open(weapon, WeaponPosition.Trap);
            }
        }
        else
        {
            if (!_firstWeapon)
            {
                WeaponGetConfirmUI.Instance.Open(weapon, WeaponPosition.Weapon1);
            }
            else if (!_secondWeapon)
            {
                WeaponGetConfirmUI.Instance.Open(weapon, WeaponPosition.Weapon2);
            }
            else
            {
                WeaponChooseUI.Instance.Open(weapon);
            }
        }
    }

    public void EquipWeapon(Weapon weapon, int position)
    {
        EquipWeapon(weapon, (WeaponPosition)position);
    }

    public void EquipWeapon(Weapon weapon, WeaponPosition position)
    {
        if (_weaponArray[(int)position] != null)
            DropWeapon(position);

        GameObject weaponGO = Instantiate(weapon.gameObject, _weaponHandler);
        _weaponArray[(int)position] = weaponGO;
        weaponGO.SetActive(false);
    }


    private void DropWeapon(WeaponPosition position)
    {
        Destroy(_weaponArray[(int)position]);
        OnStateChange?.Invoke(this, position);
    }

    public void DropTrap()
    {
        DropWeapon(WeaponPosition.Trap);
    }
}