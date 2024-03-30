using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedicalStation : MonoBehaviour
{
    private int _usesCount = 2;

    public void TryRestoreHealth(float cost)
    {
        //также проверяетс наличие эссенции
        if (_usesCount <= 0) return;
        //Восстанавливается здоровье
        //Тратится эссенция
        _usesCount -= 1;
    }
}