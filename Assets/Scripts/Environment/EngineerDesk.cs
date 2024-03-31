using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineerDesk : MonoBehaviour
{
    [SerializeField] private GameObject _chipUpgradeMenuUI;

    public void OpenChipUpgradeMenu()
    {
        _chipUpgradeMenuUI.SetActive(true);
    }
}