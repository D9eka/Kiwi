using System;
using Creatures.Player;
using UnityEngine;
using Weapons;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private Transform _weaponHandler;
    [SerializeField] private Weapon _firstWeapon;
    [SerializeField] private Weapon _secondWeapon;
    [SerializeField] private Weapon _trap;
    public Weapon CurrentWeapon { get; set; }
    public Weapon FirstWeapon => _firstWeapon;
    public Weapon SecondWeapon => _secondWeapon;
    public Weapon Trap => _trap;

    public event EventHandler OnStateChange;
    public static WeaponController Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        CurrentWeapon = Trap;
        var playerInputReader = PlayerInputReader.Instance;
        playerInputReader.OnSwitchWeapon += OnPlayerSwitchWeapon;
        playerInputReader.OnWeaponReload += OnPlayerWeaponReload;
        playerInputReader.OnAttack += OnPlayerAttack;
    }

    private void OnPlayerAttack(object sender, EventArgs e)
    {
        OnStateChange?.Invoke(this, EventArgs.Empty);
    }

    private void OnPlayerWeaponReload(object sender, EventArgs e)
    {
        OnStateChange?.Invoke(this, EventArgs.Empty);
    }

    private void OnPlayerSwitchWeapon(object sender, EventArgs e)
    {
        SwitchCurrentWeapon();
    }

    public void TryEquipWeapon(WeaponSO weaponSO)
    {
        if (weaponSO.IsConsumable)
        {
            if (!_trap)
            {
                WeaponGetConfirmUI.Instance.Open(weaponSO, _trap);
            }
            else WeaponChangeConfirmUI.Instance.Open(_trap.WeaponSO, weaponSO, _trap);
        }

        else if (!_firstWeapon)
        {
            WeaponGetConfirmUI.Instance.Open(weaponSO, _firstWeapon);
        }

        else if (!_secondWeapon)
        {
            WeaponGetConfirmUI.Instance.Open(weaponSO, _secondWeapon);
        }

        else WeaponChooseUI.Instance.Open(weaponSO);
    }

    private void EquipWeapon(WeaponSO weaponSO, ref Weapon weapon)
    {
        if (weapon) DropWeapon(ref weapon);
        var weaponObject = Instantiate(weaponSO.WeaponPrefab, _weaponHandler);
        weapon = weaponObject.GetComponent<Weapon>();
        weapon.WeaponSO = weaponSO;
        SwitchCurrentWeapon(weapon);
        OnStateChange?.Invoke(this, EventArgs.Empty);
    }

    public void EquipWeapon(WeaponSO weaponSO, Weapon weapon)
    {
        if (weapon == _firstWeapon) EquipWeapon(weaponSO, ref _firstWeapon);
        else if (weapon == _secondWeapon) EquipWeapon(weaponSO, ref _secondWeapon);
        else if (weapon == _trap) EquipWeapon(weaponSO, ref _trap);
    }


    private void DropWeapon(ref Weapon weapon)
    {
        Destroy(weapon.gameObject);
        weapon = null;
        OnStateChange?.Invoke(this, EventArgs.Empty);
    }

    public void DropConsumableWeapon()
    {
        DropWeapon(ref _trap);
    }

    private void SwitchCurrentWeapon()
    {
        if (CurrentWeapon == _firstWeapon)
        {
            if (_secondWeapon is not null) SwitchCurrentWeapon(_secondWeapon);
            else if (_trap is not null) SwitchCurrentWeapon(_trap);
        }
        else if (CurrentWeapon == _secondWeapon)
        {
            if (_trap is not null) SwitchCurrentWeapon(_trap);
            else if (_firstWeapon is not null) SwitchCurrentWeapon(_firstWeapon);
        }
        else if (CurrentWeapon == _trap)
        {
            if (_firstWeapon is not null) SwitchCurrentWeapon(_firstWeapon);
            else if (_secondWeapon is not null) SwitchCurrentWeapon(_secondWeapon);
        }

        PlayerController.Instance.SetActiveWeapon(CurrentWeapon);
        OnStateChange?.Invoke(this, EventArgs.Empty);
    }

    private void SwitchCurrentWeapon(Weapon weapon)
    {
        HideWeapon();
        CurrentWeapon = weapon;
        ShowWeapon();
        PlayerController.Instance.SetActiveWeapon(CurrentWeapon);
        OnStateChange?.Invoke(this, EventArgs.Empty);
    }

    private void HideWeapon()
    {
        CurrentWeapon.gameObject.SetActive(false);
    }

    private void ShowWeapon()
    {
        CurrentWeapon.gameObject.SetActive(true);
    }

    public void ForceEquipWeapon()
    {
        
    }
}