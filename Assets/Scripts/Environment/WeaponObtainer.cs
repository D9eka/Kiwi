using UnityEngine;
using UnityEngine.Events;
using Weapons;

public class WeaponObtainer : MonoBehaviour
{
    [SerializeField] private Weapon _weapon;
    [SerializeField] private UnityEvent _onObtain;

    public void Obtain()
    {
        WeaponController.Instance.EquipWeapon(_weapon);
        _onObtain?.Invoke();
        Destroy(gameObject);
    }
}