using Components.UI.Screens.Store;
using Creatures.Player;
using Player;
using Sections;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Weapons;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private GameObject _firstWeapon;
    [SerializeField] private GameObject _secondWeapon;
    [SerializeField] private GameObject _trap;
    [Space]
    [SerializeField] private List<WeaponSO> _weaponsSO;

    private Transform _weaponHandler;
    private GameObject[] _weaponsArray = new GameObject[3];
    private int _currentWeapon = -1;

    public Weapon CurrentWeapon => _currentWeapon != -1 && _weaponsArray[_currentWeapon] != null ? _weaponsArray[_currentWeapon].GetComponent<Weapon>() : null;
    public Weapon[] EquippedWeapons => _weaponsArray.Where(go => go != null).Select(go => go.GetComponent<Weapon>()).ToArray();

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
        if (Instance != null)
            Destroy(Instance);
        Instance = this;
    }

    private void Start()
    {
        PlayerInputReader playerInputReader = PlayerInputReader.Instance;
        playerInputReader.OnSwitchWeapon += OnPlayerSwitchWeapon;
        playerInputReader.OnAttack += OnPlayerAttack;

        PlayerVisual playerVisual = PlayerController.Instance.Visual;
        playerVisual.OnStartAttackAnimation += PlayerVisual_OnStartAttackAnimation;
        playerVisual.OnAttackAnimation += PlayerVisual_OnAttackAnimation;
        playerVisual.OnFinishAttackAnimation += PlayerVisual_OnFinishAttackAnimation;
        playerVisual.OnStartReloadAnimation += PlayerVisual_OnStartReloadAnimation;
        playerVisual.OnFinishReloadAnimation += PlayerVisual_OnFinishReloadAnimation;
        playerVisual.OnStartDeathAnimation += PlayerVisual_OnStartDeathAnimation;

        PlayerController.Instance.OnChangeLadderState += PlayerController_OnChangeLadderState;

        if (SectionManager.Instance != null)
            SectionManager.Instance.OnStartLoadingSection += SectionManager_OnStartLoadingSection;
        else
            SectionTutorial.Instance.OnStartLoadingSection += SectionManager_OnStartLoadingSection;

        _weaponHandler = PlayerController.Instance.WeaponHandler;

        GameObject[] weaponsToSpawn = Load();
        int i = 0;
        foreach (GameObject weapon in weaponsToSpawn)
        {
            if (weapon != null)
            {
                GameObject weaponGO = Instantiate(weapon, _weaponHandler);
                _weaponsArray[i] = weaponGO;
                weaponGO.SetActive(false);
            }
            i++;
        }
        if (_currentWeapon != -1)
        {
            _weaponsArray[_currentWeapon].SetActive(true);
        }
    }

    public void SwitchWeapon(int weaponIndex)
    {
        if (_currentWeapon != -1 && _weaponsArray[_currentWeapon] != null)
            _weaponsArray[_currentWeapon].SetActive(false);

        if (_currentWeapon == weaponIndex)
            _currentWeapon = -1;
        else
            _currentWeapon = weaponIndex;

        if (_currentWeapon != -1 && _weaponsArray[_currentWeapon] != null)
            _weaponsArray[_currentWeapon].SetActive(true);
    }
    public void SwitchWeapon()
    {
        if (_weaponsArray.Where(weapon => weapon != null).ToArray().Length == 0)
            return;
        do
            _currentWeapon = (int)Mathf.Repeat(_currentWeapon + 1f, _weaponsArray.Length - 1);
        while (_weaponsArray[_currentWeapon] == null);

        SwitchWeapon(_currentWeapon);
    }

    public void SwitchWeapon(WeaponPosition weaponPosition)
    {
        SwitchWeapon((int)weaponPosition);
    }

    #region Events
    private void OnPlayerAttack(object sender, EventArgs e)
    {
        OnStateChange?.Invoke(this, (WeaponPosition)_currentWeapon);
    }

    private void OnPlayerSwitchWeapon(object sender, WeaponPosition e)
    {
        SwitchWeapon(e);
    }

    private void PlayerVisual_OnStartAttackAnimation(object sender, EventArgs e)
    {
        if (_currentWeapon == -1 || _weaponsArray[_currentWeapon] == null)
            return;
        _weaponsArray[_currentWeapon].SetActive(false);
    }

    private void PlayerVisual_OnAttackAnimation(object sender, EventArgs e)
    {
        if (_currentWeapon == -1 || _weaponsArray[_currentWeapon] == null)
            return;
        if (CurrentWeapon is Melee melee)
            melee.OnAttack();
        if (CurrentWeapon is Trap trap)
            trap.OnAttack();
    }

    private void PlayerVisual_OnFinishAttackAnimation(object sender, EventArgs e)
    {
        if (_currentWeapon == -1 || _weaponsArray[_currentWeapon] == null)
            return;
        _weaponsArray[_currentWeapon].SetActive(true);
        if (CurrentWeapon is Melee melee)
            melee.OnAttack();
        if (CurrentWeapon is Trap trap)
            trap.OnAttack();
    }

    private void PlayerVisual_OnStartReloadAnimation(object sender, EventArgs e)
    {
        if (_currentWeapon == -1 || _weaponsArray[_currentWeapon] == null)
            return;
        _weaponsArray[_currentWeapon].SetActive(false);
    }

    private void PlayerVisual_OnFinishReloadAnimation(object sender, EventArgs e)
    {
        if (_currentWeapon == -1 || _weaponsArray[_currentWeapon] == null)
            return;
        _weaponsArray[_currentWeapon].SetActive(true);
        CurrentWeapon.GetComponent<Gun>().Reload();
        OnStateChange?.Invoke(this, (WeaponPosition)_currentWeapon);
    }

    private void PlayerController_OnChangeLadderState(object sender, bool e)
    {
        if (_currentWeapon == -1 || _weaponsArray[_currentWeapon] == null)
            return;
        _weaponsArray[_currentWeapon].SetActive(!e);
    }
    private void PlayerVisual_OnStartDeathAnimation(object sender, EventArgs e)
    {
        if (_currentWeapon == -1 || _weaponsArray[_currentWeapon] == null)
            return;
        _weaponsArray[_currentWeapon].SetActive(false);
    }

    private void SectionManager_OnStartLoadingSection(object sender, EventArgs e)
    {
        Save();
    }
    #endregion

    public Weapon GetWeapon(WeaponPosition position)
        => _weaponsArray[(int)position] ? _weaponsArray[(int)position].GetComponent<Weapon>() : null;

    public void TryEquipWeapon(Weapon weapon)
    {
        if (weapon.Data.Type == WeaponSO.WeaponType.Trap)
        {
            if (_weaponsArray[(int)WeaponPosition.Trap])
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
            if (_weaponsArray[0] == null)
            {
                WeaponGetConfirmUI.Instance.Open(weapon, WeaponPosition.Weapon1);
            }
            else if (_weaponsArray[1] == null)
            {
                WeaponGetConfirmUI.Instance.Open(weapon, WeaponPosition.Weapon2);
            }
            else
            {
                WeaponChooseUI.Instance.Open(weapon);
            }
        }
    }

    public void EquipWeapon(Weapon weapon)
    {
        if (weapon.Data.Type == WeaponSO.WeaponType.Trap)
        {
            EquipWeapon(weapon, WeaponPosition.Trap);
        }
        else
        {
            if (GetWeapon(WeaponPosition.Weapon1) == null)
            {
                EquipWeapon(weapon, WeaponPosition.Weapon1);
            }
            else if (GetWeapon(WeaponPosition.Weapon2) == null)
            {
                EquipWeapon(weapon, WeaponPosition.Weapon2);
            }
            else
            {
                EquipWeapon(weapon, WeaponPosition.Weapon1);
            }
        }
    }

    public void EquipWeapon(Weapon weapon, int position)
    {
        EquipWeapon(weapon, (WeaponPosition)position);
    }

    public void EquipWeapon(Weapon weapon, WeaponPosition position)
    {
        if (_weaponsArray[(int)position] != null)
            DropWeapon(position);

        GameObject weaponGO = Instantiate(weapon.gameObject, _weaponHandler);
        _weaponsArray[(int)position] = weaponGO;
        if (_currentWeapon != -1)
            _weaponsArray[_currentWeapon].SetActive(false);
        _currentWeapon = (int)position;
        _weaponsArray[_currentWeapon].SetActive(true);
        OnStateChange?.Invoke(this, position);
    }


    private void DropWeapon(WeaponPosition position)
    {
        Destroy(_weaponsArray[(int)position]);
        _weaponsArray[(int)position] = null;
        OnStateChange?.Invoke(this, position);
    }

    public void DropTrap()
    {
        DropWeapon(WeaponPosition.Trap);
    }

    private void Save()
    {
        WeaponControllerData.WeaponsArray = new WeaponSO[_weaponsArray.Length];
        for (int i = 0; i < _weaponsArray.Length; i++)
        {
            if (_weaponsArray[i] != null)
            {
                WeaponControllerData.WeaponsArray[i] = _weaponsArray[i].GetComponent<Weapon>().Data;
            }
        }
        WeaponControllerData.CurrentWeapon = _currentWeapon;
    }

    private GameObject[] Load()
    {
        GameObject[] weaponsToSpawn;
        if (WeaponControllerData.WeaponsArray == null)
            weaponsToSpawn = new GameObject[] { _firstWeapon, _secondWeapon, _trap };
        else
        {
            weaponsToSpawn = new GameObject[WeaponControllerData.WeaponsArray.Length];
            for (int i = 0; i < weaponsToSpawn.Length; i++)
            {
                if (WeaponControllerData.WeaponsArray[i] != null)
                    weaponsToSpawn[i] = WeaponControllerData.WeaponsArray[i].Prefab;
            }
            _currentWeapon = WeaponControllerData.CurrentWeapon;
        }
        return weaponsToSpawn;
    }
}

public static class WeaponControllerData
{
    public static WeaponSO[] WeaponsArray;
    public static int CurrentWeapon;

    public static void Clear()
    {
        WeaponsArray = null;
        CurrentWeapon = -1;
    }
}