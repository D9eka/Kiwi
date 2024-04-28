using System;
using System.Collections.Generic;
using UnityEngine;

public class ChipsManagerHUD_UI : MonoBehaviour
{
    [SerializeField] private List<ChipIconUI> _chipIcons;
    [SerializeField] private GameObject _chipIconPrefab;
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
                CreateNewIcon();
            }

            _chipIcons[i].SetInfo(_chipManager.ObtainedChips[i].ChipSO);
        }
    }

    private void CreateNewIcon()
    {
        var newIcon = Instantiate(_chipIconPrefab, transform).GetComponent<ChipIconUI>();
        _chipIcons.Add(newIcon);
    }
}