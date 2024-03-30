using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenStation : MonoBehaviour
{
    private bool _wasUsed;

    public void TryRestoreOxygen()
    {
        if (_wasUsed) return;
        //Восстанавливается кислород
        _wasUsed = true;
    }
}