using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscountChip : PassiveChip
{
    private const int MAX_LEVEL = 2;

    private float _discountIncreasePercent = 0.15f;

    protected override void SetValues()
    {
        _discountIncreasePercent = currentLevel switch
        {
            1 => 0.15f,
            2 => 0.3f,
            _ => _discountIncreasePercent
        };
    }

    public override void Activate()
    {
        StatsModifier.priceMultiplier -= _discountIncreasePercent;
    }

    public override void Deactivate()
    {
        StatsModifier.priceMultiplier += _discountIncreasePercent;
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