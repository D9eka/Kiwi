using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenStation : MonoBehaviour
{
    private bool _wasUsed;

    public void TryRestoreOxygen()
    {
        if (_wasUsed) return;
        GameManager.Instance.ChangeOxygenPercent(1);
        _wasUsed = true;
    }
}