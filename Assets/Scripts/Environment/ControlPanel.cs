using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPanel : MonoBehaviour
{
    [SerializeField] private SecretDoor _door;
    
    public void TryOpenSecretDoor()
    {
        //Если нет пропуску, или дверь открыта то return, иначе открываем дверь и забираем(?) пропуск
        if (_door.IsOpened) return;
        if (!GameManager.Instance.TryUseKeyCard(1)) return;
        _door.Open();
    }
}