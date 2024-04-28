using Components.Interactables;
using Components.Oxygen;
using Creatures.Player;
using UnityEngine;

public class OxygenStation : MonoBehaviour
{
    private bool _wasUsed;

    public void TryRestoreOxygen()
    {
        if (_wasUsed)
            return;
        PlayerController.Instance.GetComponent<OxygenComponent>().Restore();
        _wasUsed = true;
        SoundManager.Instance.PlaySound(SoundManager.Instance._rechargeOxygenSound);
        GetComponent<InteractableComponent>().Activate(false);
    }
}