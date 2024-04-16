using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChipBlockUI : MonoBehaviour
{
    [SerializeField] private ChipRewardUI _chipRewardUI;
    [SerializeField] private ChipSO _chipSO;
    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _description;
    public ChipSO ChipSO => _chipSO;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(Obtain);
    }

    private void SetInfo()
    {
        if (_chipSO is null) return;
        _image.sprite = _chipSO.Sprite;
        _name.text = _chipSO.Name;
        _description.text = _chipSO.Description;
        var chip = ChipCreator.Create(_chipSO);
        chip.SetChipSO(_chipSO);
    }

    public void SetChip(ChipSO chipSO)
    {
        _chipSO = chipSO;
        SetInfo();
    }

    private void Obtain()
    {
        _chipRewardUI.Obtain(this);
    }
}