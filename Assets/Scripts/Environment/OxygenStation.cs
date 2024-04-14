using Components.Oxygen;
using Creatures.Player;
using System.Collections;
using System.Collections.Generic;
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
    }
}