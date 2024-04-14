using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Weapons;

public class WeaponBlockUI : MonoBehaviour
{
    [SerializeField] private WeaponSO _weaponSO;
    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _description;

    [SerializeField] private TextMeshProUGUI _damageStatValue;

    [SerializeField] private TextMeshProUGUI _speedStatValue;
    [SerializeField] private TextMeshProUGUI _rangeStatValue;
    [SerializeField] private TextMeshProUGUI _ammoStatValue;
    [SerializeField] private TextMeshProUGUI _damageTypeValue;
    private int _cost = 1;
    public int Cost => _cost;
    public WeaponSO WeaponSO => _weaponSO;

    private void SetInfo()
    {
        if (_weaponSO is null) return;
        _image.sprite = _weaponSO.Sprite;
        _name.text = _weaponSO.Name;
        _description.text = _weaponSO.Description;
        var weapon = _weaponSO.WeaponPrefab.GetComponent<Weapon>();
        weapon.WeaponSO = _weaponSO;
        _damageStatValue.text = weapon.DamageInfo;
        _speedStatValue.text = weapon.AttackSpeed.ToString();
        switch (weapon)
        {
            case Gun gun:
                _rangeStatValue.text = "-";
                _ammoStatValue.text = gun.AmmoCapacity.ToString();
                break;
            case Melee melee:
                _rangeStatValue.text = melee.AttackRange.ToString();
                _ammoStatValue.text = "-";
                break;
            case Trap trap:
                _rangeStatValue.text = "-";
                _ammoStatValue.text = trap.MaxAmount.ToString();
                break;
        }
    }

    public void SetWeapon(WeaponSO weaponSO)
    {
        _weaponSO = weaponSO;
        SetInfo();
    }
}