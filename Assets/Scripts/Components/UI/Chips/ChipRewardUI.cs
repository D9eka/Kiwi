using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class ChipRewardUI : MonoBehaviour
{
    [SerializeField] private ChipBlockUI _chipBlockFirst;
    [SerializeField] private ChipBlockUI _chipBlockSecond;
    [SerializeField] private ChipBlockUI _chipBlockThird;
    public static ChipRewardUI Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        // ShowReward();
    }

    private void GenerateChips()
    {
        var chips = Randomiser.GetRandomElements(ChipManager.Instance.PossibleChips, 3);
        _chipBlockFirst.SetChip(chips[0]);
        _chipBlockSecond.SetChip(chips[1]);
        _chipBlockThird.SetChip(chips[2]);
    }


    public void Obtain(ChipBlockUI chipBlock)
    {
        ChipManager.Instance.ObtainChip(chipBlock.ChipSO);
        Close();
    }

    private void Close()
    {
        UIManager.Instance.TryCloseLastWindow();
    }

    public void ShowReward()
    {
        GenerateChips();
        UIManager.Instance.OpenNewWindow(GetComponent<WindowUI>());
    }
}