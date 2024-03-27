using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscountChip : Chip
{
    private const int MAX_LEVEL = 2;

    private float _discountIncreasePercent = 0.15f;

    protected override void SetValues()
    {
        switch (currentLevel)
        {
            case 1:
                _discountIncreasePercent = 0.15f;
                break;
            case 2:
                _discountIncreasePercent = 0.3f;
                break;
        }
    }

    public override void Activate()
    {
        StatsModifier.pricesMultiplier -= _discountIncreasePercent;
    }

    public override void Deactivate()
    {
        StatsModifier.pricesMultiplier += _discountIncreasePercent;
    }

    public DiscountChip(int currentLevel) : base(currentLevel)
    {
        maxLevel = MAX_LEVEL;
    }

    public DiscountChip()
    {
        maxLevel = MAX_LEVEL;
    }
}