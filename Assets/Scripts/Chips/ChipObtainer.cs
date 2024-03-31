using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChipObtainer : MonoBehaviour
{
    [SerializeField] private ChipSO _chipSO;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ChipManager.Instance.ObtainChip(_chipSO);
        }
    }
}