using UnityEngine;
using UnityEngine.Events;
using Weapons;

public class WeaponObtainer : MonoBehaviour
{
    [SerializeField] private Weapon _weapon;
    [SerializeField] private UnityEvent _onObtain;

    private void Start()
    {
        transform.rotation = new Quaternion(0f, 0f, -42f, 0f);
    }

    public void Obtain()
    {
        WeaponController.Instance.EquipWeapon(_weapon);
        _onObtain?.Invoke();
        Destroy(gameObject);
    }
}