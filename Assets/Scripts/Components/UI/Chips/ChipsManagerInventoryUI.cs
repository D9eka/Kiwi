using System;
using System.Collections.Generic;
using UnityEngine;

public class ChipsManagerInventoryUI : MonoBehaviour
{
    [SerializeField] private List<ChipIconUI> _chipIcons;
    [SerializeField] private GameObject _chipIconRowPrefab;
    private ChipManager _chipManager;

    private void Start()
    {
        _chipManager = ChipManager.Instance;
        _chipManager.OnStateChange += OnChipManagerStateChange;
        SetChipsInfo();
    }

    private void OnChipManagerStateChange(object sender, EventArgs e)
    {
        SetChipsInfo();
    }

    private void SetChipsInfo()
    {
        for (var i = 0; i < _chipManager.ObtainedChips.Count; i++)
        {
            if (_chipIcons.Count < i + 1)
            {
                CreateNewRow();
            }
            if (_chipManager != null && _chipManager.ObtainedChips[i] != null && _chipManager.ObtainedChips[i].ChipSO != null)
                _chipIcons[i].SetInfo(_chipManager.ObtainedChips[i].ChipSO);
        }
    }

    private void CreateNewRow()
    {
        var newRow = Instantiate(_chipIconRowPrefab, transform);
        var newIcons = newRow.GetComponentsInChildren<ChipIconUI>();
        foreach (var newIcon in newIcons)
        {
            _chipIcons.Add(newIcon);
        }
    }
}