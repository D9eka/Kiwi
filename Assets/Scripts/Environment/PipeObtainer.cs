using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class PipeObtainer : MonoBehaviour
{
    [SerializeField] private WeaponSO _weaponSO;
    public static PipeObtainer Instance { get; private set; }
    [SerializeField] private UnityEvent _onObtain;

    private void Awake()
    {
        Instance = this;
    }

    public void Obtain()
    {
        WeaponController.Instance.EquipWeapon(_weaponSO, null);
        Destroy(gameObject);
        _onObtain?.Invoke();
    }
}