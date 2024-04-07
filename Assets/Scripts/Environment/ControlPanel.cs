using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ControlPanel : MonoBehaviour
{
    [SerializeField] private SecretSectionDoorIn _secretSectionDoor;
    
    public void TryOpenSecretDoor()
    {
        if (_secretSectionDoor.IsOpened) return;
        if (!GameManager.Instance.TryUseKeyCard(1)) return;
        _secretSectionDoor.Open();
    }
}