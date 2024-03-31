using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscountChip : PassiveChip
{
    private float _discountIncreasePercent = 0.15f;
    private readonly List<float> _discountIncreasePercentLevels = new() { 0.15f, 0.3f };

    protected override void SetValues()
    {
        _discountIncreasePercent = _discountIncreasePercentLevels[currentLevel - 1];
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
        maxLevel = _discountIncreasePercentLevels.Count;
    }

    public DiscountChip()
    {
        maxLevel = _discountIncreasePercentLevels.Count;
    }
}