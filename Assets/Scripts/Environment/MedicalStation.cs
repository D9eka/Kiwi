using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedicalStation : MonoBehaviour
{
    private int _usesCount = 2;

    public void TryRestoreHealth(int cost)
    {
        if (_usesCount <= 0) return;
        if (!GameManager.Instance.TrySpendEssence(cost)) return;
        //Восстанавливается здоровье, по типу
        // PlayerHealth.Instance.HealPercent(0.25)
        _usesCount -= 1;
    }
}