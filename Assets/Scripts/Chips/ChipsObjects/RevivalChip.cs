using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevivalChip : Chip
{
    private const int MAX_LEVEL = 3;

    private float _restoringHealthPercent = 0.2f;


    protected override void SetValues()
    {
        _restoringHealthPercent = currentLevel switch
        {
            1 => 0.2f,
            2 => 0.4f,
            3 => 0.6f,
            _ => _restoringHealthPercent
        };
    }

    public RevivalChip(int currentLevel) : base(currentLevel)
    {
        maxLevel = MAX_LEVEL;
    }

    public RevivalChip()
    {
        maxLevel = MAX_LEVEL;
    }

    public void Revive()
    {
        ChipManager.Instance.RemoveChip(this);
        ChipManager.Instance.ObtainChip(new BrokenChip());
    }
}