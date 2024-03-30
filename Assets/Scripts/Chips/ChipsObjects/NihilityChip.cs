using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NihilityChip : PassiveChip
{
    private const int MAX_LEVEL = 3;

    private float _damageIncreasePercent = 0.3f;


    protected override void SetValues()
    {
        _damageIncreasePercent = currentLevel switch
        {
            1 => 0.3f,
            2 => 0.6f,
            3 => 0.9f,
            _ => _damageIncreasePercent
        };
    }

    public override void Activate()
    {
        StatsModifier.brokenChipDamagePercentAdder += _damageIncreasePercent;
    }

    public override void Deactivate()
    {
        StatsModifier.brokenChipDamagePercentAdder -= _damageIncreasePercent;
    }

    public NihilityChip(int currentLevel) : base(currentLevel)
    {
        maxLevel = MAX_LEVEL;
    }

    public NihilityChip()
    {
        maxLevel = MAX_LEVEL;
    }
}