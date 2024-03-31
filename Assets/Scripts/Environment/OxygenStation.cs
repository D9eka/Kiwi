using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenStation : MonoBehaviour
{
    private bool _wasUsed;
    private float _restoringOxygenPercent = 1f;


    public void TryRestoreOxygen()
    {
        if (_wasUsed) return;
        GameManager.Instance.ChangeOxygenPercent(_restoringOxygenPercent);
        _wasUsed = true;
    }
}