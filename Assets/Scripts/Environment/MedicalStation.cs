using Components.Health;
using Components.Interactables;
using Creatures.Player;
using UnityEngine;

public class MedicalStation : MonoBehaviour
{
    private int _usesCount = 2;
    private float restoringHealthPercent = 0.25f;

    public void TryRestoreHealth(int cost)
    {
        if (_usesCount <= 0 || !MyGameManager.TrySpendEssence(cost))
            return;
        if (PlayerController.Instance.TryGetComponent<HealthComponent>(out var healthComponent))
        {
            healthComponent.ModifyHealthPercent(restoringHealthPercent);
        }

        _usesCount -= 1;
        if (_usesCount == 0) GetComponent<InteractableComponent>().Activate(false);
        SoundManager.Instance.PlaySound(SoundManager.Instance._rechargeHealthSound);
    }
}