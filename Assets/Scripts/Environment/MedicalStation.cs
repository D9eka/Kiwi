using System.Collections;
using System.Collections.Generic;
using Components.Health;
using Creatures.Player;
using UnityEngine;

public class MedicalStation : MonoBehaviour
{
    private int _usesCount = 2;
    private float restoringHealthPercent = 0.25f;

    public void TryRestoreHealth(int cost)
    {
        if (_usesCount <= 0) return;
        if (!GameManager.Instance.TrySpendEssence(cost)) return;
        if (PlayerController.Instance.TryGetComponent<HealthComponent>(out var healthComponent))
        {
            healthComponent.ModifyHealthPercent(restoringHealthPercent);
        }
        _usesCount -= 1;
    }
}