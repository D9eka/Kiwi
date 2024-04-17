using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NihilityChip : PassiveChip
{
    private float _damageIncreasePercent = 0.3f;
    private readonly List<float> _damageIncreasePercentLevels = new List<float> { 0.3f, 0.6f, 0.9f };


    protected override void SetValues()
    {
        _damageIncreasePercent = _damageIncreasePercentLevels[currentLevel - 1];
    }

    public override void Activate()
    {
        StatsModifier.BrokenChipDamagePercentAdder += _damageIncreasePercent;
    }

    public override void Deactivate()
    {
        StatsModifier.BrokenChipDamagePercentAdder -= _damageIncreasePercent;
    }

    public NihilityChip(int currentLevel) : base(currentLevel)
    {
        maxLevel = _damageIncreasePercentLevels.Count;
    }

    public NihilityChip()
    {
        maxLevel = _damageIncreasePercentLevels.Count;
    }
}